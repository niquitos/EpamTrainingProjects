using Microsoft.AspNetCore.Authorization;

namespace BasicAuthentication.CustomPolicyProvider
{
    public class SecurityLevelRequirement : IAuthorizationRequirement
    {
        public int Level { get; }
        public SecurityLevelRequirement(int level)
        {
            Level = level;
        }
    }
}
