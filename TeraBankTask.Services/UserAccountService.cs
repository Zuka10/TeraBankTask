using TeraBankTask.DTO;
using TeraBankTask.Facade.Repositories;
using TeraBankTask.Facade.Services;

namespace TeraBankTask.Services;

public class UserAccountService : IUserAccountService
{
    private readonly IUnitOfWork unitOfWork;

    public UserAccountService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public void Deposit(string accountNumber, decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than zero.", nameof(amount));
        }

        var user = GetByAccountNumber(accountNumber);
        if (user != null)
        {
            user.Balance += amount;
            unitOfWork.SaveChanges();
        }
    }

    public void Withdraw(string accountNumber, decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than zero.", nameof(amount));
        }

        var user = GetByAccountNumber(accountNumber);
        if (user != null)
        {
            if (amount > user.Balance)
            {
                throw new ArgumentException("Amount is more than a balance");
            }
            else
            {
                user.Balance -= amount;
                unitOfWork.SaveChanges();
            }
        }
    }

    public void Transfer(string senderAccountNumber, string recepientAccountNumber, decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than zero.", nameof(amount));
        }

        var sender = GetByAccountNumber(senderAccountNumber) ?? throw new ArgumentException("Sender account not found");
        var recepient = GetByAccountNumber(recepientAccountNumber) ?? throw new ArgumentException("Recepient account not found");

        if (sender.Balance >= amount)
        {
            sender.Balance -= amount;

            recepient.Balance += amount;

            unitOfWork.SaveChanges();
        }
        else
        {
            throw new InvalidOperationException("Insufficient funds for transfer.");
        }
    }

    public object GetAccount(int userId)
    {
        var userAccount = unitOfWork.UserAccountRepository.Get(userId);
        if (userAccount != null)
        {
            var userAccountInfo = new UserAccountInfoDTO
            {
                AccountNumber = userAccount.AccountNumber,
                Balance = userAccount.Balance
            };

            return userAccountInfo;
        }

        throw new ArgumentNullException("User account not found");
    }

    public UserAccount GetByAccountNumber(string accountNumber)
    {
        var userAccount = unitOfWork.UserAccountRepository.Find(e => e.AccountNumber == accountNumber);
        return userAccount;
    }
}

public class UserAccountInfoDTO
{
    public string AccountNumber { get; set; } = null!;
    public decimal Balance { get; set; }
}