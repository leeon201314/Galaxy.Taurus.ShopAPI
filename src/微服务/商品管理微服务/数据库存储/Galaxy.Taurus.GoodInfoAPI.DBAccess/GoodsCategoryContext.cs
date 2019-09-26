using Galaxy.Taurus.DBUtil;
using Galaxy.Taurus.GoodInfoAPI.DBAccess;
using Galaxy.Taurus.GoodInfoAPI.Entitys;
using Galaxy.Taurus.GoodInfoAPI.IDBAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxy.Taurus.ShopInfoAPI.DBAccess
{
    public class GoodsCategoryContext : BaseContext<GoodsCategory>, IGoodsCategoryContext
    {
        private DbSet<ShopInfoExtensions> shopInfoExtensionsDbSet { get; set; }

        public List<GoodsCategory> GetByShopId(string shopId)
        {
            return CurrentDbSet.Where(c => c.ShopId == shopId).OrderBy(c => c.ShowIndex).ToList();
        }       

        /// <summary>
        /// 以乐观锁的形式实现添加分组
        /// </summary>
        public async Task<DataResult> AddGoodsCategory(GoodsCategory goodsCategory)
        {
            ShopInfoExtensions shopInfoExtensions = await shopInfoExtensionsDbSet.FirstOrDefaultAsync(sh => sh.ShopId == goodsCategory.ShopId);
            int categoryCount = await CurrentDbSet.CountAsync(gr => gr.ShopId == goodsCategory.ShopId);

            if (categoryCount >= shopInfoExtensions.LimitCategoryNum)
                return new DataResult { Code = DataResultCode.Fail, Message = $"最多只能拥有{shopInfoExtensions.LimitCategoryNum}个商品分组！" };

            using (IDbContextTransaction transaction = Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                CurrentDbSet.Add(goodsCategory);

                string sql = $"update ShopInfoExtensions set CategoryDataVersion = @newVersion where shopId = @shopId and CategoryDataVersion = @oldVersion";
                int result = this.Database.ExecuteSqlCommand(sql, new[]
                {
                         new MySqlParameter("newVersion", Guid.NewGuid().ToString("N")),
                         new MySqlParameter("oldVersion", shopInfoExtensions.CategoryDataVersion),
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

            return new DataResult { Code = DataResultCode.Success, Message = "添加成功！" };
        }
    }
}
