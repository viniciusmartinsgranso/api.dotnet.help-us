using Microsoft.AspNetCore.Mvc;

namespace api.dotnet.help_us.Modules.Auth.Jwt;

public class UserContextAttribute : TypeFilterAttribute
{
    public UserContextAttribute() : base(typeof(UserContextFilter)) { }
}