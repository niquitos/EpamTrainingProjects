using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SecurityAPI.Controllers
{
    public class SecretController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return Ok("secret message");
        }
    }
}
