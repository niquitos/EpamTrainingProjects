using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace BasicAuthentication.AuthorizationRequirement
{
    public static class CookieJarAuthOperations
    {
        public static OperationAuthorizationRequirement Open => new()
        {
            Name = CookieJarOperations.Open
        };
        public static OperationAuthorizationRequirement Look => new()
        {
            Name = CookieJarOperations.Look
        };
        public static OperationAuthorizationRequirement ComeNear => new()
        {
            Name = CookieJarOperations.ComeNear
        };
        public static OperationAuthorizationRequirement TakeCookie => new()
        {
            Name = CookieJarOperations.TakeCookie
        };
    }
}
