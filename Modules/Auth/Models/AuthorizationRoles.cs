using api.dotnet.help_us.Modules.Users.Models;
using Microsoft.AspNetCore.Authorization;

namespace api.dotnet.help_us.Modules.Auth.Models
{

    public class AuthorizationRoles : AuthorizeAttribute
    {
        public AuthorizationRoles(params RolesEnum[] roles)
        {
            Roles = string.Join(",", roles.Select(r => r.ToString()));
        }
    }
}