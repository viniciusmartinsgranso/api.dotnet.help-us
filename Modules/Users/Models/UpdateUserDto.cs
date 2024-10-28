using api.dotnet.help_us.Data;

namespace api.dotnet.help_us.Modules.Users.Models;

public class UpdateUserDto : BaseUpdateCrud
{
    public new string? Name { get; set; }
    public new string? Email { get; set; }
    public new string? Password { get; set; }
    public new string? City { get; set; }
    public new List<RolesEnum>? Roles { get; set; }
}