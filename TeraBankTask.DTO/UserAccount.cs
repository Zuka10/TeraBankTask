using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeraBankTask.DTO;

public class UserAccount
{
    public int Id { get; set; }

    [MinLength(9), MaxLength(9)]
    public string AccountNumber { get; set; } = null!;
    public decimal Balance { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}