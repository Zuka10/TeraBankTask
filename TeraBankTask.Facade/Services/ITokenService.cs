using TeraBankTask.DTO;

namespace TeraBankTask.Facade.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}