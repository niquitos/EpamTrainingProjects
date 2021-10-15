using IdentityModel.OidcClient;
using System.Threading.Tasks;

namespace WpfClient.Infrastructure.Interfaces
{
    public interface IAuthorizationService
    {
        Task<LoginResult> AuthorizeAsync();
    }
}
