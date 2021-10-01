using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;

namespace BasicAuthentication.CustomPolicyProvider
{
    public static class DynamicAuthorizationPolicyFactory
    {
        public static AuthorizationPolicy Create(string policyName)
        {
            var parts = policyName.Split('.');
            var type = parts.First();
            var value = parts.Last();

            return type switch
            {
                DynamicPolicies.Rank => 
                new AuthorizationPolicyBuilder()
                    .RequireClaim("Rank", value)
                    .Build(),

                DynamicPolicies.SecurityLevel => 
                new AuthorizationPolicyBuilder()
                    .AddRequirements(new SecurityLevelRequirement(Convert.ToInt32(value)))
                    .Build(),

                _ => null,
            };
        }
    }
}
