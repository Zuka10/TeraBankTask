using TeraBankTask.DTO;

namespace TeraBankTask.Facade.Services;

public interface IUserService
{
    void Create(User user);
    void Update(User user);
    void Delete(User user);
    User GetById(int id);
    IEnumerable<User> GetAll();
    User? Authenticate(string email, string password);
    bool IsValidEmail(string email);
}