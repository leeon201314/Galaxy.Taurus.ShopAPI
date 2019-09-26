using Galaxy.Taurus.FileAPI.Entitys;
using Galaxy.Taurus.FileAPI.IBusiness;
using Galaxy.Taurus.FileAPI.IDBRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galaxy.Taurus.FileAPI.Business
{
    public class FileGroupBusiness : IFileGroupBusiness
    {
        public IFileGroupContext fileGroupContext;

        public FileGroupBusiness(IFileGroupContext fileGroupContext)
        {
            this.fileGroupContext = fileGroupContext;
        }

        public Task<DataResult> Add(FileGroup fileGroup)
        {
            fileGroup.Id = Guid.NewGuid().ToString("N");
            return fileGroupContext.AddFileGroup(fileGroup);
        }

        public FileGroup Update(FileGroup fileGroup)
        {
            fileGroupContext.Update(fileGroup);
            return fileGroupContext.SingleOrDefault(f => f.Id == fileGroup.Id);
        }

        public bool Delete(string shopId, string groupId)
        {
            fileGroupContext.DeleteSingle(f => f.Id == groupId && f.ShopId == shopId);
            return true;
        }

        public Task<FileGroup> Get(string shopId, string groupId)
        {
            return fileGroupContext.SingleOrDefaultAsync(f => f.Id == groupId && f.ShopId == shopId);
        }

        public Task<List<FileGroup>> GetByShopId(string shopId)
        {
            return fileGroupContext.ListAsync(f => f.ShopId == shopId);
        }
    }
}
