using Galaxy.Taurus.GoodInfoAPI.Entitys.DTO;
using System.Threading.Tasks;

namespace Galaxy.Taurus.GoodInfoAPI.IBusiness
{
    public interface IGoodInfoExtensionsBusiness
    {
        Task<GoodInfoExtensionsDTO> GetById(string shopId, string goodId);

        void Update(GoodInfoExtensionsDTO goodInfoExtensionsDTO);
    }
}
