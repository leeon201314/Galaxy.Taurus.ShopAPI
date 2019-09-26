using Galaxy.Taurus.AuthorizationAPI.Entitys;

namespace Galaxy.Taurus.AuthorizationAPI.IBusiness
{
    public interface IMemberBusiness
    {
        Member GetByPhoneNumber(string phoneNumber);

        void AddMember(Member member);
    }
}
