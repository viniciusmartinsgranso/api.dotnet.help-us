using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.dotnet.help_us.Data;
using api.dotnet.help_us.Modules.Users.Models;

namespace api.dotnet.help_us.Modules.Users.Entities;

[Table("users")]
public class UserEntity: BaseEntity
{
    [Required, MaxLength(128)]
    public string Name { get; set; }
    
    [Required, EmailAddress]
    public string Email { get; set; }

    [Required] 
    public List<RolesEnum> Roles { get; set; }
    
    [Required]
    public string Password { get; set; }
}