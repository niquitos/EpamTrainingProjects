using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;

namespace MvcClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> SecretAsync()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var idToken = await HttpContext.GetTokenAsync("id_token");
            var refreshToken = await HttpContext.GetTokenAsync("refresh_token");

            var jwtAccessToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
            var jwtIdToken = new JwtSecurityTokenHandler().ReadJwtToken(idToken);
            //var jwtRefreshToken = new JwtSecurityTokenHandler().ReadJwtToken(refreshToken);

            var claims = User.Claims;
            var result = await GetSecret(accessToken);

            return View(model: new { result });
        }

        private async Task<string> GetSecret(string accessToken)
        {
            var apiClient = _httpClientFactory.CreateClient();

            apiClient.SetBearerToken(accessToken);

            var response = await apiClient.GetAsync("https://localhost:44312/secret");

           return await response.Content.ReadAsStringAsync();
        }
    }
}
