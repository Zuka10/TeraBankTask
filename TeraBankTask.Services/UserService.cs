using MediatR;
using TeraBankTask.DTO;
using TeraBankTask.Facade.Repositories;
using TeraBankTask.Facade.Services;
using TeraBankTask.Services.Commands.UserService;
using TeraBankTask.Services.Queries.UserService;

namespace TeraBankTask.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public UserService(IUnitOfWork unitOfWork, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async void Create(User user)
    {
        await _mediator.Send(new CreateUserCommand(user));
    }

    public async void Update(User user)
    {
        await _mediator.Send(new UpdateUserCommand(user));
    }

    public async void Delete(User user)
    {
        await _mediator.Send(new DeleteUserCommand(user));
    }

    public User GetById(int id)
    {
        var query = new GetUserByIdQuery(id);
        return _mediator.Send(query).Result;
    }

    public IEnumerable<User> GetAll()
    {
        var query = new GetAllUserQuery();
        return _mediator.Send(query).Result;
    }

    public User? Authenticate(string email, string password)
    {
        var user = GetByEmail(email);

        if (user != null)
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

    private User GetByEmail(string email)
    {
        return _unitOfWork.UserRepository.Find(e => e.Email == email);
    }
}