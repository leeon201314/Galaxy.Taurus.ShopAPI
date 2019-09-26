using AutoMapper;
using Galaxy.Taurus.OrderingAPI.Entitys;
using Galaxy.Taurus.OrderingAPI.ViewModel;

namespace Galaxy.Taurus.OrderingAPI.Util
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
                cfg.CreateMap<Order, OrderDTO>().ReverseMap();
            });

            config.AssertConfigurationIsValid();
            return config.CreateMapper();
        }
    }
}
