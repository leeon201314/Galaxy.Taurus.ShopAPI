using Galaxy.Taurus.DBUtil;
using Galaxy.Taurus.GoodInfoAPI.DBAccess;
using Galaxy.Taurus.GoodInfoAPI.Entitys;
using Galaxy.Taurus.GoodInfoAPI.IDBAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Galaxy.Taurus.ShopInfoAPI.DBAccess
{
    public class ShopInfoExtensionsContext : BaseContext<ShopInfoExtensions>, IShopInfoExtensionsContext
    {
        private DbSet<GoodInfo> goodInfoDbSet { get; set; }

        private DbSet<GoodsCategory> goodsCategoryDbSet { get; set; }

        public override void DeleteSingle(Expression<Func<ShopInfoExtensions, bool>> predicate)
        {
            using (IDbContextTransaction transaction = Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    var shopInfoExtensions = CurrentDbSet.SingleOrDefault(predicate);
                    string shopId = shopInfoExtensions.ShopId;

                    var goodsCategorys = goodsCategoryDbSet.Where(fg => fg.ShopId == shopId).ToList();
                    if (goodsCategorys != null && goodsCategorys.Count > 0)
                    {
                        foreach (var categoryItem in goodsCategorys)
                        {
                            goodsCategoryDbSet.Remove(categoryItem);
                        }
                    }

                    var goodInfos = goodInfoDbSet.Where(fg => fg.ShopId == shopId).ToList();
                    if (goodInfos != null && goodInfos.Count > 0)
                    {  
                        foreach (var goodItem in goodInfos)
                        {
                            goodInfoDbSet.Remove(goodItem);
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
