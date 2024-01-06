using System.ComponentModel.DataAnnotations;

namespace TeraBankTask.API.Modules;

public class RegisterViewModel
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;

    [MinLength(8, ErrorMessage = "Password must contain 8 or more character"), MaxLength(16, ErrorMessage = "Password must contain less than 16 character")]
    public string Password { get; set; } = null!;
}
