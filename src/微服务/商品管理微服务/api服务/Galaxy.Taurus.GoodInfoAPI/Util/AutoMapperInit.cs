using AutoMapper;
using Galaxy.Taurus.GoodInfoAPI.Entitys;
using Galaxy.Taurus.GoodInfoAPI.Entitys.DTO;
using Galaxy.Taurus.GoodInfoAPI.RequestParams;
using Galaxy.Taurus.GoodInfoAPI.ViewModel;

namespace Galaxy.Taurus.GoodInfoAPI.Util
{
    /// <summary>
    /// AutoMapper辅助类
    /// </summary>
    public class AutoMapperUtil
    {
        private static object _padlock = new object();
        private static IMapper _mapper;

        /// <summary>
        /// 单例
        /// </summary>
        public static IMapper Singleton
        {
            get
            {
                if (_mapper == null)
                {
                    lock (_padlock)
                    {
                        if (_mapper == null)
                        {
                            _mapper = Init();
                        }
                    }
                }

                return _mapper;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private static IMapper Init()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GoodInfoExtensionsDTO, GoodDetailInfo>()
                    .ForMember(dest => dest.BasicInfo, opt => opt.Ignore())
                    .ForMember(dest => dest.SKUList, opt => opt.Ignore())
                    .ReverseMap();

                cfg.CreateMap<GoodBasicInfoDTO, GoodInfo>().ReverseMap();

                cfg.CreateMap<GoodInfoSKUDTO, GoodInfoSKU>().ReverseMap();

                cfg.CreateMap<DataResult, ResultViewModel>().ReverseMap();

                cfg.CreateMap<GetGoodPriceParams, GoodPriceViewModel>().ReverseMap();
            });

            config.AssertConfigurationIsValid();
            return config.CreateMapper();
        }
    }
}
