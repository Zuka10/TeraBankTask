using MediatR;
using TeraBankTask.DTO;

namespace TeraBankTask.Services.Commands.UserService;

public record UpdateUserCommand(User user) : IRequest<User>;