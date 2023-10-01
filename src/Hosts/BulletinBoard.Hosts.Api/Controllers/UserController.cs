using BulletinBoard.Hosts.Api.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Hosts.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("public")]
        public JsonResult Public()
        {
            return new JsonResult("Public");
        }

        [Authorize]
        //[Authorize(Roles = "Role")]
        [Authorize(Policy = "CustomPolicy")]
        [HttpPost("requiring_authorization")]
        public JsonResult RequiringAuthorization()
        {
            return new JsonResult("Success!");
        }

        [HttpPost("get_user_info")]
        public UserDto GetUserInfo()
        {
            return new UserDto
            {
                Scheme = HttpContext.User.Identity.AuthenticationType,
                IsAuthenticated = HttpContext.User.Identity.IsAuthenticated,
                Claims = HttpContext.User.Claims.Select(claim => (object) new {claim.Type, claim.Value}).ToList()
            };
        }
    }
}
