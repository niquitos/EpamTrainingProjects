using System.Collections.Generic;

namespace BasicAuthentication.CustomPolicyProvider
{
    public static class DynamicPolicies
    {
        public const string SecurityLevel = "SecurityLevel";
        public const string Rank = "Rank";

        public static IEnumerable<string> Get()
        {
            yield return SecurityLevel;
            yield return Rank;
        }
    }
}
