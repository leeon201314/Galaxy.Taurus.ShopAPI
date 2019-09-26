using AutoMapper;
using Galaxy.Taurus.AuthorizationAPI.Entitys;
using Galaxy.Taurus.AuthorizationAPI.ViewModel;

namespace Galaxy.Taurus.AuthorizationAPI.Util
{
    /// <summary>
    /// AutoMapperUtil
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
                cfg.CreateMap<UserInfo, UserInfoViewModel>().ReverseMap();
            });

            config.AssertConfigurationIsValid();
            return config.CreateMapper();
        }
    }
}
