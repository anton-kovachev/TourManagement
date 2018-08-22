using Microsoft.AspNetCore.Authorization;

namespace TourManagement.API.Authorization
{
    public class RoleAuthorizationRequirement : IAuthorizationRequirement
    {
        public string Role { get; set; }

        public RoleAuthorizationRequirement(string roleName)
        {
            Role = roleName;
        }
    }
}