using Galaxy.Taurus.FileAPI.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galaxy.Taurus.FileAPI.IBusiness
{
    public interface IFileGroupBusiness
    {
        Task<DataResult> Add(FileGroup fileGroup);

        FileGroup Update(FileGroup fileGroup);

        bool Delete(string shopId, string groupId);

        Task<FileGroup> Get(string shopId, string groupId);

        Task<List<FileGroup>> GetByShopId(string shopId);
    }
}
