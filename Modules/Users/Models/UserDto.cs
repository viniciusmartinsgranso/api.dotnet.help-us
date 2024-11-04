using System.Text.Json.Serialization;
using api.dotnet.help_us.Data;
using api.dotnet.help_us.Modules.Users.Entities;

namespace api.dotnet.help_us.Modules.Users.Models;

public class UserDto
{
    [JsonConstructor]
    public UserDto(Guid id, DateTime createdAt, DateTime updatedAt, string name, string email, List<RolesEnum> roles)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Name = name;
        Email = email;
        Roles = roles;
    }

    public UserDto(UserEntity user) : this(user.Id, user.CreatedAt, user.UpdatedAt, user.Name, user.Email, user.Roles)
    {
    }
    
    public Guid Id { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }
    
    public List<RolesEnum> Roles { get; set; }

}