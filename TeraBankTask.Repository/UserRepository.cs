using TeraBankTask.DTO;
using TeraBankTask.Facade.Repositories;
using TeraBankTask.Repository;

namespace TeraBankTask.Repositories;

public class UserRepository(TeraBankTaskDbContext context) : RepositoryBase<User>(context), IUserRepository { }