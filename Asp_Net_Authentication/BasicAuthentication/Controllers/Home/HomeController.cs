using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace BasicAuthentication.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        [Authorize(Policy = "Claim.DoB")]
        public IActionResult SecretPolicy()
        {
            return View("Secret");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult SecretAdmin()
        {
            return View("Secret");
        }

        public IActionResult Authenticate()
        {
            var grandmaClaims = new List<Claim>()
            {
               new Claim(ClaimTypes.Name, "Bob"),
               new Claim(ClaimTypes.Email, "Bob@fmail.com"),
               new Claim("Grandma says", "Very nice boy")
            };

            var licenseClaims = new List<Claim>()
            {
               new Claim(ClaimTypes.Name, "Bob K Foo"),
               new Claim("DrivingLicense", "A+")
            };

            var customClaims = new List<Claim>
            {
               new Claim(ClaimTypes.DateOfBirth, "11/11/01"),
               new Claim(ClaimTypes.Role, "Admin")
            };

            var grandmaIdentity = new ClaimsIdentity(grandmaClaims, "Grandma Identity");
            var licenseIdentity = new ClaimsIdentity(licenseClaims, "Government");
            var customIdentity = new ClaimsIdentity(customClaims, "CustomClaims");

            var userPrinciple = new ClaimsPrincipal( new ClaimsIdentity[] { grandmaIdentity, licenseIdentity, customIdentity });

            HttpContext.SignInAsync(userPrinciple);

            return RedirectToAction("Index");
        }
    }
}
