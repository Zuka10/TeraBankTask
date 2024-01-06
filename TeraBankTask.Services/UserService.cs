using TeraBankTask.DTO;
using TeraBankTask.Facade.Repositories;
using TeraBankTask.Facade.Services;

namespace TeraBankTask.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void Create(User user)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

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

        user.Account = userAccount;

        _unitOfWork.UserRepository.Insert(user);
        _unitOfWork.UserAccountRepository.Insert(userAccount);

        _unitOfWork.SaveChanges();
    }

    public void Update(User user)
    {
        var existingUser = _unitOfWork.UserRepository.Get(user.Id);
        existingUser.UserName = user.UserName;
        existingUser.Email = user.Email;
        _unitOfWork.UserRepository.Update(existingUser);
        _unitOfWork.SaveChanges();
    }

    public void Delete(User user)
    {
        _unitOfWork.UserRepository.Get(user.Id);
        _unitOfWork.UserRepository.Delete(user);
        _unitOfWork.SaveChanges();
    }

    public User GetById(int id)
    {
        var user = _unitOfWork.UserRepository.Get(id);

        if (user != null)
        {
            var userWithoutPassword = new User
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Account = user.Account
            };

            return userWithoutPassword;
        }

        return null;
    }

    public IEnumerable<User> GetAll()
    {
        var users = _unitOfWork.UserRepository
            .GetAll()
            .Select(u => new User
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                Account = u.Account
            });

        return users;
    }

    public User? Authenticate(string email, string password)
    {
        var user = GetByEmail(email);

        if(user != null)
        {
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if (isValidPassword)
            {
                return user;
            }
        }

        return null;
    }

    public bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false;
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }

    private string GenerateRandomAccountNumber()
    {
        Random rand = new Random();
        return rand.Next(100000000, 999999999).ToString();
    }

    private bool Exists(string accountNumber)
    {
        var exists = _unitOfWork.UserAccountRepository.Find(u => u.AccountNumber == accountNumber);

        if(exists != null)
        {
            return true;
        }

        return false;
    }

    private User GetByEmail(string email)
    {
        return _unitOfWork.UserRepository.Find(e => e.Email == email);
    }
}