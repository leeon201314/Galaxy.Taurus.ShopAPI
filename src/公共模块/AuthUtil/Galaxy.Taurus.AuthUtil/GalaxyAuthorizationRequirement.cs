using Microsoft.AspNetCore.Authorization;

namespace Galaxy.Taurus.AuthUtil
{
    public class GalaxyAuthorizationRequirement : IAuthorizationRequirement
    {
        public string Opration { get; private set; }

        public GalaxyAuthorizationRequirement(string opration)
        {
            Opration = opration;
        }
    }
}
