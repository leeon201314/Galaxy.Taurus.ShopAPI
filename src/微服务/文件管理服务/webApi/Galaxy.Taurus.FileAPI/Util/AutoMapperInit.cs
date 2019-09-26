using AutoMapper;
using Galaxy.Taurus.FileAPI.ViewModels;

namespace Galaxy.Taurus.ShopInfoAPI.Util
{
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
        private static IMapper Init()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MessageViewModelProfile>();
            });

            config.AssertConfigurationIsValid();
            return config.CreateMapper();
        }
    }
}
