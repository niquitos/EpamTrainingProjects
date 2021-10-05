using Microsoft.AspNetCore.Authorization;

namespace BasicAuthentication.AuthorizationRequirement
{
    public static class AuthorizationPolicyBuilderExtension
    {
        public static AuthorizationPolicyBuilder RequireCustomClaim(this AuthorizationPolicyBuilder builder, string claimType)
        {
            builder.AddRequirements(new CustomRequireClaim(claimType));

            return builder;
        }
    }
}
