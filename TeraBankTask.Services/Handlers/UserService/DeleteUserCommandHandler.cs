using MediatR;
using TeraBankTask.DTO;
using TeraBankTask.Facade.Repositories;
using TeraBankTask.Services.Commands.UserService;

namespace TeraBankTask.Services.Handlers.UserService;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        _unitOfWork.UserRepository.Get(request.user.Id);
        _unitOfWork.UserRepository.Delete(request.user);
        _unitOfWork.SaveChanges();

        return Task.FromResult(true);
    }
}
