using Galaxy.Taurus.DBUtil;
using Galaxy.Taurus.FileAPI.Entitys;
using Galaxy.Taurus.FileAPI.IDBRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Taurus.FileAPI.DBRepository
{
    public class FileDataContext : BaseContext<FileData>, IFileDataContext
    {
        private DbSet<ShopInfoExtensions> shopInfoExtensionsDbSet { get; set; }

        public Task<List<FileData>> GetByGroup(string shopId, string groupId, out int total, int pageSize = 20, int pageIndex = 1)
        {
            total = 0;

            if (!string.IsNullOrEmpty(groupId) && groupId != "-1")
            {
                return GetPageAsync(f => f.GroupId == groupId && f.ShopId == shopId, f => f.VersionDateTime, pageIndex, pageSize, out total);
            }
            else
            {
                StringBuilder sqlBuilder = new StringBuilder();
                sqlBuilder.Append(" SELECT * FROM FileData WHERE NOT EXISTS ");
                sqlBuilder.Append(" (SELECT GroupId FROM FileGroup WHERE FileGroup.Id = FileData.GroupId AND FileGroup.ShopId = @shopId) ");
                sqlBuilder.Append(" AND FileData.ShopId = @shopId ");

                List<MySqlParameter> sqlParams = new List<MySqlParameter>();
                sqlParams.Add(new MySqlParameter("shopId", shopId));
                
                total = CurrentDbSet.FromSql(sqlBuilder.ToString(), sqlParams.ToArray()).CountAsync().Result;

                sqlBuilder.Append(" ORDER BY FileData.VersionDateTime DESC ");
                sqlBuilder.Append($" LIMIT {(pageIndex - 1) * pageSize}, {pageSize} ");

                return CurrentDbSet.FromSql(sqlBuilder.ToString(), sqlParams.ToArray()).ToListAsync();
            }
        }

        public async Task<DataResult> AddFileData(FileData fileData)
        {
            ShopInfoExtensions shopInfoExtensions = await shopInfoExtensionsDbSet.FirstOrDefaultAsync(sh => sh.ShopId == fileData.ShopId);
            int fileCount = await CurrentDbSet.CountAsync(gr => gr.ShopId == fileData.ShopId);

            if (fileCount >= shopInfoExtensions.LimitFileNum)
                return new DataResult { Code = DataResultCode.Fail, Message = $"该商店最多只能拥有{shopInfoExtensions.LimitFileNum}张图片！" };

            using (IDbContextTransaction transaction = Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                CurrentDbSet.Add(fileData);

                string sql = $"update ShopInfoExtensions set FileDataVersion = @newVersion where shopId = @shopId and FileDataVersion = @oldVersion";
                int result = this.Database.ExecuteSqlCommand(sql, new[]
                {
                         new MySqlParameter("newVersion", Guid.NewGuid().ToString("N")),
                         new MySqlParameter("oldVersion", shopInfoExtensions.FileDataVersion),
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
