using BulletinBoard.Hosts.Api.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Hosts.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользовательскими методами.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Публичный метод.
        /// </summary>
        /// <returns><see cref="JsonResult"/></returns>
        [AllowAnonymous]
        [HttpPost("public")]
        public JsonResult Public()
        {
            return new JsonResult("Public");
        }

        /// <summary>
        /// Метод требующий авторизацию пользователя.
        /// </summary>
        /// <returns><see cref="JsonResult"/></returns>
        [Authorize]
        //[Authorize(Roles = "Role")]
        //[Authorize(Policy = "CustomPolicy")]
        [HttpPost("requiring_authorization")]
        public JsonResult RequiringAuthorization()
        {
            return new JsonResult("Success!");
        }

        /// <summary>
        /// Метод для получения информации о пользователе.
        /// </summary>
        /// <returns><see cref="AuthUserDto"/></returns>
        [HttpPost("get_user_info")]
        public AuthUserDto GetUserInfo()
        {
            return new AuthUserDto
            {
                Scheme = HttpContext.User.Identity.AuthenticationType,
                IsAuthenticated = HttpContext.User.Identity.IsAuthenticated,
                Claims = HttpContext.User.Claims.Select(claim => (object) new {claim.Type, claim.Value}).ToList()
            };
        }
    }
}
