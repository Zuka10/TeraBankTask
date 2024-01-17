using MediatR;
using TeraBankTask.DTO;
using TeraBankTask.Facade.Repositories;
using TeraBankTask.Services.Queries.UserService;

namespace TeraBankTask.Services.Handlers.UserService;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = _unitOfWork.UserRepository.Get(request.id);

        if (user != null)
        {
            var userWithoutPassword = new User
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Account = user.Account
            };

            return Task.FromResult(userWithoutPassword);
        }

        return null;
    }
}
