namespace api.dotnet.help_us.Modules.Auth.Models;

public class TokenDto(string token)
{
    public string Token { get; set; } = token;
}