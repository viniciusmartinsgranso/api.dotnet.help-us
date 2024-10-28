using System.Security.Claims;
using System.Text.Json;
using api.dotnet.help_us.Data;
using api.dotnet.help_us.Modules.Auth.Models;
using api.dotnet.help_us.Modules.Users.Entities;
using api.dotnet.help_us.Modules.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.dotnet.help_us.Modules.Users.Controller
{
    [Route("users")]
    [ApiController]
    public class UsersController(AppDbContext context) : ControllerBase
    {
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await context.Users.ToListAsync();
            
            if (users.Count.Equals(0))
            {
                return NoContent();
            }
            
            return Ok(users.Select(u => new UserDto(u)).ToList());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound("Usuário não foi encontrado.");
            }

            return Ok(new UserDto(user));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto payload)
        {
            var hasUser = await context.Users.AnyAsync(u => u.Email == payload.Email);

            if (hasUser)
            {
                return Conflict(new { Message = "Já existe um usuário com esse e-mail" });
            }
            
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(payload.Password);
            
            var user = new UserEntity
            {
                Name = payload.Name,
                Email = payload.Email,
                Password = passwordHash,
                Roles = payload.Roles ?? new List<RolesEnum> { RolesEnum.User },
            };
            
            context.Users.Add(user);
            
            await context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, new UserDto(user));
        }

        [HttpPatch("{id}")]
        [AuthorizationRoles(RolesEnum.User, RolesEnum.User)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserDto payload)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null) return NotFound();
            
            if (!string.IsNullOrEmpty(payload.Email) && payload.Email != user.Email)
            {
                var hasEmail = await context.Users.AnyAsync(pa => pa.Email == payload.Email);

                if (hasEmail)
                {
                    return Conflict("Já existe um usuário com esse e-mail cadastrado");
                }
            }
            
            if (!string.IsNullOrEmpty(payload.Name)) user.Name = payload.Name;
            if (!string.IsNullOrEmpty(payload.Email)) user.Email = payload.Email;
            if (!string.IsNullOrEmpty(payload.Password)) user.Password = payload.Password;
            if (payload.Roles != null) user.Roles = payload.Roles;
            
            user.UpdatedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("email/{email}")]
        [AuthorizationRoles(RolesEnum.Admin)]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            return Ok(new UserDto(user));
        }

        [HttpPatch("toggle/{id}")]
        [AuthorizationRoles(RolesEnum.Admin, RolesEnum.User)]
        public async Task<IActionResult> Toggle(Guid id)
        {
            var user = await context.Users.FindAsync(id);
            
            if (user == null)
            {
                throw new Exception("Usuário não encontrado");
            }
            
            user.IsActive = !user.IsActive;
            
            context.Users.Update(user);
            
            await context.SaveChangesAsync();
            
            return Ok(new UserDto(user));
        }
        
        [HttpDelete("{id}")]
        [AuthorizationRoles(RolesEnum.Admin)]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var user = await context.Users.FirstAsync(u => u.Id == id);

            if (user == null)
            {
                throw new Exception("Usuário não encontrado");
            }
            
            context.Users.Remove(user);
            
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("delete/{name}")]
        [AuthorizationRoles(RolesEnum.Admin)]
        public async Task<IActionResult> DeleteByName(string name)
        {
            var user = await context.Users.FirstAsync(u => u.Name == name);

            if (user == null)
            {
                throw new Exception("Usuário não encontrado");
            }
            
            context.Users.Remove(user);
            
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
