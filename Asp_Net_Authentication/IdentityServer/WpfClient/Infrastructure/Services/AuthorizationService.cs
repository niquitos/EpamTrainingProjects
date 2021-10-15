using IdentityModel.OidcClient;
using System;
using System.Threading.Tasks;
using WpfClient.Infrastructure.Interfaces;

namespace WpfClient.Infrastructure.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private OidcClient _oidcClient = null;

        public AuthorizationService(OidcClientOptions options)
        {
            _oidcClient = new OidcClient(options);
        }

        public async Task<LoginResult> AuthorizeAsync()
        {

            LoginResult result;

            try
            {
                result = await _oidcClient.LoginAsync();
            }

            catch (Exception ex)
            {
                return new LoginResult() { Error = "Exception", ErrorDescription = ex.Message };
            }

            return result;
        }
    }
}
