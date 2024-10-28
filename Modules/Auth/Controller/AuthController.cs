using api.dotnet.help_us.Data;
using api.dotnet.help_us.Modules.Auth.Jwt;
using api.dotnet.help_us.Modules.Auth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.dotnet.help_us.Modules.Auth.Controller
{
    [Route("auth")]
    [ApiController]
    public class AuthController(AppDbContext context, JwtService jwtService) : ControllerBase
    {
        [HttpPost("local")]
        public async Task<IActionResult> Login([FromBody] LoginDto payload)
        {
            var user = await context.Users.SingleOrDefaultAsync(u => u.Email == payload.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(payload.Password, user.Password))
            {
                return Unauthorized("Email ou senha inválidos.");
            }

            var token = jwtService.GenerateToken(user);
            return Ok(new TokenDto(token));
        }
    }
}
