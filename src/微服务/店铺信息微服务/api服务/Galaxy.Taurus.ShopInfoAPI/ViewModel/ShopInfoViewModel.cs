using AutoMapper;
using Galaxy.Taurus.ShopInfoAPI.Entitys;

namespace Galaxy.Taurus.ShopInfoAPI.ViewModel
{
    public class ShopInfoViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }


        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string ShopDesc { get; set; }

        /// <summary>
        /// 联系地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string LinkPhone { get; set; }

        /// <summary>
        /// 客服QQ
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// 客服微信
        /// </summary>
        public string WeChat { get; set; }

        /// <summary>
        /// 营业时间
        /// </summary>
        public string OpeningHours { get; set; }
    }

    public class ShopInfoViewModelProfile : Profile
    {
        public ShopInfoViewModelProfile()
        {
            base.CreateMap<ShopInfo, ShopInfoViewModel>().ReverseMap();
        }
    }
}
