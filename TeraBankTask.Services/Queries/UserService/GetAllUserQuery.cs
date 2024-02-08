using MediatR;
using TeraBankTask.DTO;

namespace TeraBankTask.Services.Queries.UserService;

public record GetAllUserQuery() : IRequest<IEnumerable<User>>;