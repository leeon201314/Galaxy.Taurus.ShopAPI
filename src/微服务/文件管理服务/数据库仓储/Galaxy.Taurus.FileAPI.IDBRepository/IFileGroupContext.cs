using Galaxy.Taurus.FileAPI.Entitys;
using Galaxy.Taurus.IDBUtil;
using System.Threading.Tasks;

namespace Galaxy.Taurus.FileAPI.IDBRepository
{
    public interface IFileGroupContext : IBaseContext<FileGroup>
    {
        Task<DataResult> AddFileGroup(FileGroup fileGroup);
    }
}
