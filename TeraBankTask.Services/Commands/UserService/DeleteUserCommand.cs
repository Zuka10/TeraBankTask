using MediatR;
using TeraBankTask.DTO;

namespace TeraBankTask.Services.Commands.UserService;

public record DeleteUserCommand(User user) : IRequest<bool>;