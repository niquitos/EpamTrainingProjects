using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Controllers
{
    public class OAuthController : Controller
    {
        [HttpGet]
        public IActionResult Authorize(string response_type, string client_id, string redirect_uri,
                                       string scope, string state)
        {
            var query = new QueryBuilder();
            query.Add("redirectUri", redirect_uri);
            query.Add("state", state);

            return View(model: query.ToString());
        }

        [HttpPost]
        public IActionResult Authorize(string username, string redirectUri, string state)
        {
            const string code = "BABAABABABA";

            var query = new QueryBuilder();
            query.Add("code", code);
            query.Add("state", state);

            return Redirect($"{redirectUri}{query}");
        }

        public IActionResult Token(string grant_type, string code, string redirect_uri, string client_id)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,"some_id"),
                new Claim("granny","cookie")
            };

            var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);
            var key = new SymmetricSecurityKey(secretBytes);
            var algorithm = SecurityAlgorithms.HmacSha256;

            var signingCredentials = new SigningCredentials(key, algorithm);
            var token = new JwtSecurityToken(Constants.Issuer, Constants.Audience, claims, DateTime.Now,
                                             DateTime.Now.AddHours(1), signingCredentials);
            var access_token = new JwtSecurityTokenHandler().WriteToken(token);

            var responseObject = new
            {
                access_token = access_token,
                token_type = "Bearer",
                expires_in = 3600
            };

            return Ok(responseObject);
        }

        [Authorize]
        public IActionResult Validate()
        {
            if (HttpContext.Request.Query.TryGetValue("access_token", out var token))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
