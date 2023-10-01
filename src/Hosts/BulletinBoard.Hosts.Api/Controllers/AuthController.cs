using BulletinBoard.Hosts.Api.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulletinBoard.Hosts.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<bool> Login(AuthDto dto)
        {
            // Check user credentials

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, dto.Login));
            claims.Add(new Claim("User", "User"));
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return true;
        }

        [HttpPost]
        [Route("logout")]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
