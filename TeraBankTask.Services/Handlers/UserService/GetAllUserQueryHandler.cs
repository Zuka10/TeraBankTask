using MediatR;
using TeraBankTask.DTO;
using TeraBankTask.Facade.Repositories;
using TeraBankTask.Services.Queries.UserService;

namespace TeraBankTask.Services.Handlers.UserService;

public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, IEnumerable<User>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllUserQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<IEnumerable<User>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
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

        return Task.FromResult(users);
    }
}
