using AutoMapper;
using Galaxy.Taurus.AuthorizationAPI.Entitys;

namespace Galaxy.Taurus.AuthorizationAPI.ViewModel
{
    public class UserInfoViewModel
    {
        /// <summary>
        /// 32位GUID
        /// </summary>
        public string Id { get; set; }

        public string PhoneNumber { get; set; }

        public string UserName { get; set; }
    }

    public class UserInfoProfile : Profile
    {
        public UserInfoProfile()
        {
            base.CreateMap<UserInfo, UserInfoViewModel>().ReverseMap();
        }
    }
}
