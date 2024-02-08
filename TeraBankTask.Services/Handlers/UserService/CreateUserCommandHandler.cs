using MediatR;
using TeraBankTask.DTO;
using TeraBankTask.Facade.Repositories;
using TeraBankTask.Services.Commands.UserService;

namespace TeraBankTask.Services.Handlers.UserService;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly IUnitOfWork unitOfWork;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        request.user.Password = BCrypt.Net.BCrypt.HashPassword(request.user.Password);

        string accountNumber;
        do
        {
            accountNumber = GenerateRandomAccountNumber();
        } while (Exists(accountNumber));

        var userAccount = new UserAccount
        {
            AccountNumber = accountNumber,
            Balance = 0
        };

        request.user.Account = userAccount;

        unitOfWork.UserRepository.Insert(request.user);
        unitOfWork.UserAccountRepository.Insert(userAccount);

        unitOfWork.SaveChanges();

        return Task.FromResult(request.user);
    }

    private string GenerateRandomAccountNumber()
    {
        Random rand = new Random();
        return rand.Next(100000000, 999999999).ToString();
    }

    private bool Exists(string accountNumber)
    {
        var exists = unitOfWork.UserAccountRepository.Find(u => u.AccountNumber == accountNumber);

        if (exists != null)
        {
            return true;
        }

        return false;
    }
}
