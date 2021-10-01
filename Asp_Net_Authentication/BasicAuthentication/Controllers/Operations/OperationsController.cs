using BasicAuthentication.AuthorizationRequirement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasicAuthentication.Controllers.Operations
{
    public class OperationsController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        public OperationsController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> Open()
        {
            var cookieJar = new CookieJar();//get cookie jar from db
            
            if((await _authorizationService.AuthorizeAsync(User, cookieJar, CookieJarAuthOperations.Open)).Succeeded)
            {
                return RedirectToAction("Secret", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
