using MediatR;
using TeraBankTask.DTO;
using TeraBankTask.Facade.Repositories;
using TeraBankTask.Services.Commands.UserService;

namespace TeraBankTask.Services.Handlers.UserService;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = _unitOfWork.UserRepository.Get(request.user.Id);
        existingUser.UserName = request.user.UserName;
        existingUser.Email = request.user.Email;
        _unitOfWork.UserRepository.Update(existingUser);
        _unitOfWork.SaveChanges();

        return Task.FromResult(request.user);
    }
}
