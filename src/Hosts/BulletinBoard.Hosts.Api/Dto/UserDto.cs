using System.Security.Claims;

namespace BulletinBoard.Hosts.Api.Dto
{
    public class UserDto
    {
        public bool IsAuthenticated { get; set; }
        public string Scheme { get; set; }
        public List<object> Claims { get; set; } = new List<object>();
    }
}
