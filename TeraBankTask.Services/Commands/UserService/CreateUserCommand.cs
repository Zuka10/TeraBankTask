using MediatR;
using TeraBankTask.DTO;

namespace TeraBankTask.Services.Commands.UserService;

public record CreateUserCommand(User user) : IRequest<User>;