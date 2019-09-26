using AutoMapper;
using Galaxy.Taurus.ShopInfoAPI.ViewModel;

namespace Galaxy.Taurus.ShopInfoAPI.Util
{
    public class AutoMapperInit
    {
        public void Init()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<ShopInfoViewModelProfile>();
            });
        }
    }
}
