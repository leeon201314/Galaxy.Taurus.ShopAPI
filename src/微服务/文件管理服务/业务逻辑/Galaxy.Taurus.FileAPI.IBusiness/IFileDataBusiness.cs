using Galaxy.Taurus.FileAPI.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galaxy.Taurus.FileAPI.IBusiness
{
    public interface IFileDataBusiness
    {
        Task<DataResult> Add(FileData file);

        FileData UpdateGroup(string shopId, string fileId, string groupId);

        bool Delete(string shopId, string fileId);

        FileData Get(string shopId, string fileId);

        Task<List<FileData>> GetByGroup(string shopId, string groupId, out int total, int pageSize = 20, int pageIndex = 1);
    }
}
