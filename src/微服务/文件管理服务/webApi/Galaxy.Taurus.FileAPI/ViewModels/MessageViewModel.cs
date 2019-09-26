using AutoMapper;
using Galaxy.Taurus.FileAPI.Entitys;

namespace Galaxy.Taurus.FileAPI.ViewModels
{
    public class MessageViewModel
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }

    public class MessageCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        public const int Success = 0;

        /// <summary>
        /// 参数错误
        /// </summary>
        public const int ParamsError = 301;

        /// <summary>
        /// 失败
        /// </summary>
        public const int Fail = 400;

        /// <summary>
        /// 未登录
        /// </summary>
        public const int NoLogin = 401;
    }

    public class MessageViewModelProfile : Profile
    {
        public MessageViewModelProfile()
        {
            base.CreateMap<DataResult, MessageViewModel>().ReverseMap();
        }
    }
}
