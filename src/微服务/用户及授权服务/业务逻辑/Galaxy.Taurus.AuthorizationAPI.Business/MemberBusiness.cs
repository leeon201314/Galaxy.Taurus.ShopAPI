using Galaxy.Taurus.AuthorizationAPI.Entitys;
using Galaxy.Taurus.AuthorizationAPI.IBusiness;
using Galaxy.Taurus.AuthorizationAPI.IDBAccess;

namespace Galaxy.Taurus.AuthorizationAPI.Business
{
    public class MemberBusiness : IMemberBusiness
    {
        private IMemberContext memberContext;

        public MemberBusiness(IMemberContext memberContext)
        {
            this.memberContext = memberContext;
        }

        public Member GetByPhoneNumber(string phoneNumber)
        {
            return memberContext.FirstOrDefault(m => m.PhoneNumber == phoneNumber);
        }

        public void AddMember(Member member)
        {
            memberContext.Add(member);
        }
    }
}
