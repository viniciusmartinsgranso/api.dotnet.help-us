using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace api.dotnet.help_us.Modules.Users.Models;

public class CreateUserDto
{
    [
        Required(AllowEmptyStrings = false, ErrorMessage = "É necessário enviar o nome para criar o usuário"),
        MaxLength(128, ErrorMessage = "O nome não pode ter mais que 128 caracteres."),
    ]
    public string Name { get; set; }
    
    [
        Required(AllowEmptyStrings = false, ErrorMessage = "É necessário informar o e-mail."),
        EmailAddress,
    ]
    public string Email { get; set; }
    
    [
        Required(ErrorMessage = "É necessário informar a senha."), 
        MinLength(6, ErrorMessage = "A senha precisa ter no mínimo 6 caracteres."),
        MaxLength(60, ErrorMessage = "A senha não pode ter mais que 60 caracteres.")
    ]
    public string Password { get; set; }
    
    [
        DefaultValue(RolesEnum.User)
    ]
    public List<RolesEnum>? Roles { get; set; } = new List<RolesEnum> { RolesEnum.User };
}