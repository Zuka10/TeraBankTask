using MediatR;
using TeraBankTask.DTO;

namespace TeraBankTask.Services.Queries.UserService;

public record GetUserByIdQuery(int id) : IRequest<User>;