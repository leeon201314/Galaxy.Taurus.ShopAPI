using Galaxy.Taurus.FileAPI.Entitys;
using Galaxy.Taurus.IDBUtil;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galaxy.Taurus.FileAPI.IDBRepository
{
    public interface IFileDataContext : IBaseContext<FileData>
    {
        Task<List<FileData>> GetByGroup(string shopId, string groupId, out int total, int pageSize = 20, int pageIndex = 0);

        Task<DataResult> AddFileData(FileData fileData);
    }
}
