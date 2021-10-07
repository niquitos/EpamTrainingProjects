using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiTwo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            var serverClient = _httpClientFactory.CreateClient();

            var discoverDocument = await serverClient.GetDiscoveryDocumentAsync("https://localhost:44360/");

            var tokenResponse = await serverClient.RequestClientCredentialsTokenAsync(
               new ClientCredentialsTokenRequest
                {
                    Address = discoverDocument.TokenEndpoint,
                    ClientId = "client",
                    ClientSecret = "secret",
                    Scope = "ApiOne"
               });

            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            var response = await apiClient.GetAsync("https://localhost:44312/secret");

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }
            
            var content = await response.Content.ReadAsStringAsync();

            return Ok(new
            {
                message = content
            });
        }
    }
}
