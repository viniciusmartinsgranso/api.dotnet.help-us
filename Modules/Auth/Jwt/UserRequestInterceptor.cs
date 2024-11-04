using System.Security.Claims;
using System.Text.Json;
using api.dotnet.help_us.Modules.Users.Entities;
using api.dotnet.help_us.Modules.Users.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace api.dotnet.help_us.Modules.Auth.Jwt;

public class UserContextFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var httpContext = context.HttpContext;
        var user = httpContext.User;

        if (user?.Identity?.IsAuthenticated == true)
        {
            var userJson = user.FindFirst("UserEntity")?.Value;
            
            if (!string.IsNullOrEmpty(userJson))
            {
                try
                {
                    var userDto = JsonSerializer.Deserialize<UserDto>(userJson);

                    httpContext.Items["requestUser"] = userDto;
                }
                catch (JsonException ex)
                {
                    Console.WriteLine("Erro ao desserializar UserEntity: " + ex.Message);
                }
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}