using Galaxy.Taurus.DBUtil;
using Galaxy.Taurus.FileAPI.Entitys;
using Galaxy.Taurus.FileAPI.IDBRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Galaxy.Taurus.FileAPI.DBRepository
{
    public class ShopInfoExtensionsContext : BaseContext<ShopInfoExtensions>, IShopInfoExtensionsContext
    {
        private DbSet<FileData> fileDataDbSet { get; set; }

        private DbSet<FileGroup> fileGroupDbSet { get; set; }

        public override void DeleteSingle(Expression<Func<ShopInfoExtensions, bool>> predicate)
        {
            using (IDbContextTransaction transaction = Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    var shopInfoExtensions = CurrentDbSet.SingleOrDefault(predicate);
                    string shopId = shopInfoExtensions.ShopId;

                    var fileGroups = fileGroupDbSet.Where(fg => fg.ShopId == shopId).ToList();
                    if (fileGroups != null && fileGroups.Count > 0)
                    {
                        foreach (var groupItem in fileGroups)
                        {
                            fileGroupDbSet.Remove(groupItem);
                        }
                    }

                    var fileDatas = fileDataDbSet.Where(fg => fg.ShopId == shopId).ToList();
                    if (fileDatas != null && fileDatas.Count > 0)
                    {
                        foreach (var dataItem in fileDatas)
                        {
                            fileDataDbSet.Remove(dataItem);
                        }
                    }

                    CurrentDbSet.Remove(shopInfoExtensions);
                    this.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
