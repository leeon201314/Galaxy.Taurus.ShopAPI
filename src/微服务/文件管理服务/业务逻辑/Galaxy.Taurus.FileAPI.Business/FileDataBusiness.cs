using Galaxy.Taurus.FileAPI.Entitys;
using Galaxy.Taurus.FileAPI.IBusiness;
using Galaxy.Taurus.FileAPI.IDBRepository;
using Galaxy.Taurus.FileAPI.ServiceConfig;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galaxy.Taurus.FileAPI.Business
{
    public class FileDataBusiness : IFileDataBusiness
    {
        private IFileDataContext fileDataContext;

        public FileDataBusiness(IFileDataContext fileDataContext)
        {
            this.fileDataContext = fileDataContext;
        }

        public Task<DataResult> Add(FileData file)
        {
            return fileDataContext.AddFileData(file);
        }

        public bool Delete(string shopId, string fileId)
        {
            var fileData = fileDataContext.SingleOrDefault(f => f.FileId == fileId && f.ShopId == shopId);

            if (fileData != null)
            {
                string basePath = $"{ServiceConfigInfo.Single.MainDirectory}/{fileData.ShopId}";
                string fullPath = $"{basePath}/{fileId}.{fileData.FileType}";

                if (System.IO.File.Exists(fullPath))
                    System.IO.File.Delete(fullPath);

                fileDataContext.DeleteSingle(f => f.FileId == fileId && f.ShopId == shopId);
            }

            return true;
        }

        public FileData Get(string shopId, string fileId)
        {
            return fileDataContext.SingleOrDefault(f => f.FileId == fileId && f.ShopId == shopId);
        }

        public Task<List<FileData>> GetByGroup(string shopId, string groupId, out int total, int pageSize = 20, int pageIndex = 0)
        {
            return fileDataContext.GetByGroup(shopId, groupId, out total, pageSize, pageIndex);
        }

        public FileData UpdateGroup(string shopId, string fileId, string groupId)
        {
            var file = fileDataContext.SingleOrDefault(f => f.FileId == fileId && f.ShopId == shopId);

            if (file != null)
            {
                file.GroupId = groupId;
                file.VersionDateTime = DateTime.Now;
                fileDataContext.Update(file);
                return fileDataContext.SingleOrDefault(f => f.FileId == file.FileId);
            }

            return null;
        }
    }
}
