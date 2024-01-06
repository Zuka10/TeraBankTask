using TeraBankTask.DTO;

namespace TeraBankTask.Facade.Services;

public interface IUserAccountService
{
    void Deposit(string accountNumber, decimal amount);
    void Withdraw(string accountNumber, decimal amount);
    void Transfer(string sender, string recepient, decimal amount);
    object GetAccount(int userId);
    UserAccount GetByAccountNumber(string accountNumber);
}