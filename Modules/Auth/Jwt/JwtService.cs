using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using api.dotnet.help_us.Modules.Users.Entities;
using api.dotnet.help_us.Modules.Users.Models;
using Microsoft.IdentityModel.Tokens;

namespace api.dotnet.help_us.Modules.Auth.Jwt;

public class JwtService(IConfiguration config)
{

    public string GenerateToken(UserEntity user)
    {
        var userDto = new UserDto(user);

        var userJson = JsonSerializer.Serialize(userDto);

        var claims = new List<Claim>
        {
            new Claim("UserEntity", userJson)
        };

        claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.ToString())));
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}