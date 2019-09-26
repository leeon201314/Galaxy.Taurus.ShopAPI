using Galaxy.Taurus.DBUtil;
using Galaxy.Taurus.FileAPI.Entitys;
using Galaxy.Taurus.FileAPI.IDBRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Galaxy.Taurus.FileAPI.DBRepository
{
    public class FileGroupContext : BaseContext<FileGroup>, IFileGroupContext
    {
        private DbSet<ShopInfoExtensions> shopInfoExtensionsDbSet { get; set; }

        /// <summary>
        /// 以乐观锁的形式实现添加分组
        /// </summary>
        public async Task<DataResult> AddFileGroup(FileGroup fileGroup)
        {
            ShopInfoExtensions shopInfoExtensions = await shopInfoExtensionsDbSet.FirstOrDefaultAsync(sh => sh.ShopId == fileGroup.ShopId);
            int groupCount = await CurrentDbSet.CountAsync(gr => gr.ShopId == fileGroup.ShopId);

            if (groupCount >= shopInfoExtensions.LimitGroupNum)
                return new DataResult { Code = DataResultCode.Fail, Message = $"最多只能拥有{shopInfoExtensions.LimitGroupNum}个分组！" };

            using (IDbContextTransaction transaction = Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                CurrentDbSet.Add(fileGroup);

                string sql = $"update ShopInfoExtensions set GroupDataVersion = @newVersion where shopId = @shopId and GroupDataVersion = @oldVersion";
                int result = this.Database.ExecuteSqlCommand(sql, new[]
                {
                         new MySqlParameter("newVersion", Guid.NewGuid().ToString("N")),
                         new MySqlParameter("oldVersion", shopInfoExtensions.GroupDataVersion),
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
