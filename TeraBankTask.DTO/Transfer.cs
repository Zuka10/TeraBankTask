namespace TeraBankTask.DTO;

public class Transfer
{
    public int Id { get; set; }
    public required decimal Amount { get; set; }

    public required UserAccount UserAccount { get; set; }
}
