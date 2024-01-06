using System.ComponentModel.DataAnnotations;

namespace TeraBankTask.DTO;

public class User
{
    public int Id { get; set; }
    [MinLength(8), MaxLength(16)]
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;

    public UserAccount? Account { get; set; }
}