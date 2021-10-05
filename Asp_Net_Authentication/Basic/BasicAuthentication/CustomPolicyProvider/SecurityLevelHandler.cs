using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BasicAuthentication.CustomPolicyProvider
{
    public class SecurityLevelHandler : AuthorizationHandler<SecurityLevelRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SecurityLevelRequirement requirement)
        {
            Claim claim = context.User.Claims.FirstOrDefault(x => x.Type == DynamicPolicies.SecurityLevel);

            int claimValue = claim == null ? 0 : Convert.ToInt32(claim.Value); 

            if (requirement.Level <= claimValue)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
