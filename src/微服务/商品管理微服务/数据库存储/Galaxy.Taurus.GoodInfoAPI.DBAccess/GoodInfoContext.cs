using Galaxy.Taurus.DBUtil;
using Galaxy.Taurus.GoodInfoAPI.Entitys;
using Galaxy.Taurus.GoodInfoAPI.Entitys.DTO;
using Galaxy.Taurus.GoodInfoAPI.IDBAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxy.Taurus.GoodInfoAPI.DBAccess
{
    public class GoodInfoContext : BaseContext<GoodInfo>, IGoodInfoContext
    {
        private DbSet<GoodInfoExtensions> goodInfoExtensionsDbSet { get; set; }

        private DbSet<GoodsCategory> goodsCategoryDbSet { get; set; }

        private DbSet<GoodInfoSKU> goodInfoSKUDbSet { get; set; }

        private DbSet<ShopInfoExtensions> shopInfoExtensionsDbSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<GoodInfo>()
                .Property(e => e.RecommendStatus)
                .HasConversion(new BoolToZeroOneConverter<int>());

            modelBuilder
                .Entity<GoodInfo>()
                .Property(e => e.Status)
                .HasConversion(new BoolToZeroOneConverter<int>());
        }

        /// <summary>
        /// 获取商品信息
        /// </summary>
        /// <param name="shopId">店铺ID</param>
        /// <param name="categoryId">分类ID</param>
        /// <param name="status">状态：是否上线 0:下线  1：上线 其他：所有</param>
        /// <param name="recommendStatus">推荐状态：是否推荐 0:未推荐  1：推荐 其他：所有</param>
        public PageDataDTO<List<GoodInfo>> GetByFilter(string shopId, string categoryId, int status, int recommendStatus, int pageSize, int pageIndex)
        {
            IQueryable<GoodInfo> query = CurrentDbSet.Where(c => c.ShopId == shopId);

            if (!string.IsNullOrEmpty(categoryId))
            {
                ///未分类
                if (categoryId == "-1")
                {
                    var categoryList = goodsCategoryDbSet.Where(c => c.ShopId == shopId).ToList();
                    List<string> categoryIdList = new List<string>();
                    foreach (var item in categoryList)
                    {
                        categoryIdList.Add(item.Id);
                    }

                    query = query.Where(c => !categoryIdList.Contains(c.CategoryId));
                }
                else
                {
                    query = query.Where(c => c.CategoryId == categoryId);
                }
            }

            if (status == 0 || status == 1)
            {
                query = query.Where(c => c.Status == (status == 1 ? true : false));
            }

            if (recommendStatus == 0 || recommendStatus == 1)
            {
                query = query.Where(c => c.RecommendStatus == (recommendStatus == 1 ? true : false));
            }

            int count = query.Count();

            if (pageSize > 0)
            {
                query = query.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
            }

            return new PageDataDTO<List<GoodInfo>>
            {
                Total = count,
                Data = query.OrderBy(g => g.ShowIndex).ToList()
            };
        }

        /// <summary>
        /// 添加商品信息
        /// </summary>
        /// <param name="goodInfo">基本信息</param>
        /// <param name="goodInfoExtensions">扩展信息</param>
        public async Task<DataResult> AddGoodInfo(GoodInfo goodInfo, GoodInfoExtensions goodInfoExtensions, List<GoodInfoSKU> skuList)
        {
            ShopInfoExtensions shopInfoExtensions = await shopInfoExtensionsDbSet.FirstOrDefaultAsync(sh => sh.ShopId == goodInfo.ShopId);
            int goodCount = await CurrentDbSet.CountAsync(gr => gr.ShopId == goodInfo.ShopId);

            if (goodCount >= shopInfoExtensions.LimitGoodNum)
                return new DataResult { Code = DataResultCode.Fail, Message = $"最多只能拥有{shopInfoExtensions.LimitCategoryNum}个商品！" };


            using (IDbContextTransaction transaction = Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    await CurrentDbSet.AddAsync(goodInfo);
                    await goodInfoExtensionsDbSet.AddAsync(goodInfoExtensions);
                    await goodInfoSKUDbSet.AddRangeAsync(skuList);

                    string sql = $"update ShopInfoExtensions set GoodDataVersion = @newVersion where shopId = @shopId and GoodDataVersion = @oldVersion";
                    int result = this.Database.ExecuteSqlCommand(sql, new[]
                    {
                         new MySqlParameter("newVersion", Guid.NewGuid().ToString("N")),
                         new MySqlParameter("oldVersion", shopInfoExtensions.GoodDataVersion),
                         new MySqlParameter("shopId", shopInfoExtensions.ShopId),
                });

                    if (result <= 0)
                    {
                        transaction.Rollback();
                        return new DataResult { Code = DataResultCode.Fail, Message = "添加失败，请重试！" };
                    }

                    SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    return new DataResult { Code = DataResultCode.Fail, Message = "添加失败，请重试！" };
                }
            }

            return new DataResult { Code = DataResultCode.Success, Message = "添加成功！" };
        }

        public void DeleteGoodInfo(string shopId, string goodId)
        {
            using (IDbContextTransaction transaction = Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    var basicInfo = CurrentDbSet.FirstOrDefault(g => g.ShopId == shopId && g.Id == goodId);
                    CurrentDbSet.Remove(basicInfo);

                    var extensionsInfo = goodInfoExtensionsDbSet.FirstOrDefault(g => g.ShopId == shopId && g.GoodInfoId == goodId);
                    goodInfoExtensionsDbSet.Remove(extensionsInfo);

                    var skuList = goodInfoSKUDbSet.Where(sku => sku.ShopId == shopId || sku.GoodInfoId == goodId).ToList();
                    goodInfoSKUDbSet.RemoveRange(skuList);

                    SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }
    }
}
