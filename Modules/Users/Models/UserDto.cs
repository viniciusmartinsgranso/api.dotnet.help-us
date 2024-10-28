using api.dotnet.help_us.Data;
using api.dotnet.help_us.Modules.Users.Entities;

namespace api.dotnet.help_us.Modules.Users.Models;

public class UserDto(UserEntity user)
{
    public Guid Id { get; set; } = user.Id;
    
    public DateTime CreatedAt { get; set; } = user.CreatedAt;
    
    public DateTime UpdatedAt { get; set; } = user.UpdatedAt;

    public string Name { get; set; } = user.Name;
    
    public string Email { get; set; } = user.Email;
    
    public List<RolesEnum> Roles { get; set; } = user.Roles;
}