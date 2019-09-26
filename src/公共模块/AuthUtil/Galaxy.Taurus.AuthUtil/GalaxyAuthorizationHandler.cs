using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galaxy.Taurus.AuthUtil
{
    public class GalaxyAuthorizationHandler : AuthorizationHandler<GalaxyAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GalaxyAuthorizationRequirement requirement)
        {
            var opListStr = context.User.FindFirst("opList")?.Value;

            if (!string.IsNullOrEmpty(opListStr))
            {
                string[] opList = opListStr.Split("_");
               
                foreach (var op in opList)
                {
                    if(op.ToLower() == requirement.Opration.ToLower())
                    {
                        context.Succeed(requirement);
                        return Task.CompletedTask;
                    }
                }
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}
