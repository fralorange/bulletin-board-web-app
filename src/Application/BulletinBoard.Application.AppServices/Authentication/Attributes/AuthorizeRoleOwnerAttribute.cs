using BulletinBoard.Application.AppServices.Contexts.Ad.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace BulletinBoard.Contracts.Attributes
{
    /// <summary>
    /// Атрибут проверки, что пользователь редактирует своё объявление, либо пользователь имеет конкретную роль.
    /// </summary>
    public class AuthorizeRoleOwnerAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Инициализация атрибута.
        /// </summary>
        /// <param name="role">Роль.</param>
        public AuthorizeRoleOwnerAttribute(string role) : base(typeof(AuthorizeRoleOwnerFilter))
        {
            Arguments = new object[] { role };
        }

        private class AuthorizeRoleOwnerFilter : IAuthorizationFilter
        {
            private readonly IAdService _adService;
            private readonly string _role;

            public AuthorizeRoleOwnerFilter(IAdService adService, string role)
            {
                _adService = adService;
                _role = role;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var user = context.HttpContext.User;

                var entityId = Guid.Parse(context!.RouteData!.Values["id"]!.ToString()!);
                var entity = _adService.GetByIdAsync(entityId, context.HttpContext.RequestAborted).Result;

                if (entity == null)
                    return;

                if (entity!.User.Id.ToString() != user.FindFirstValue(ClaimTypes.NameIdentifier) && user.FindFirstValue(ClaimTypes.Role) != _role) 
                { 
                    context.Result = new ContentResult
                    {
                        StatusCode = 403,
                        Content = "User lacks sufficient permissions or is not the owner of the entity being edited.",
                        ContentType = "text/plain",
                    };
                }
            }
        }
    }
}
