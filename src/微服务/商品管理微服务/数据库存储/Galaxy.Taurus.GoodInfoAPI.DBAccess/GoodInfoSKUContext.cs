using Galaxy.Taurus.DBUtil;
using Galaxy.Taurus.GoodInfoAPI.Entitys;
using Galaxy.Taurus.GoodInfoAPI.IDBAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxy.Taurus.GoodInfoAPI.DBAccess
{
    public class GoodInfoSKUContext : BaseContext<GoodInfoSKU>, IGoodInfoSKUContext
    {
        public async Task<bool> DeleteAndAddSKU(List<GoodInfoSKU> skuList)
        {
            bool res = false;

            using (IDbContextTransaction transaction = Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    List<GoodInfoSKU> removeList = await CurrentDbSet
                        .Where(sku => sku.ShopId == skuList[0].ShopId && sku.GoodInfoId == skuList[0].GoodInfoId)
                        .ToListAsync();
                    CurrentDbSet.RemoveRange(removeList);
                    await CurrentDbSet.AddRangeAsync(skuList);

                    SaveChanges();
                    transaction.Commit();
                    res = true;
                }
                catch
                {
                    transaction.Rollback();
                    res = false;
                }
            }

            return res;
        }

        public Task<List<GoodInfoSKU>> GetByGoodId(string shopId,string goodId)
        {
            return CurrentDbSet.Where(sku => sku.ShopId == shopId && sku.GoodInfoId == goodId)
                .OrderBy(sku=>sku.ShowIndex)
                .ToListAsync();
        }
    }
}
