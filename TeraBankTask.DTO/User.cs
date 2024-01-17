using System.ComponentModel.DataAnnotations;

namespace TeraBankTask.DTO;

public class User
{
    public int Id { get; set; }
    [MinLength(8), MaxLength(16)]
    public required string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;
    public required string Email { get; set; } = null!;

    public UserAccount? Account { get; set; }
}