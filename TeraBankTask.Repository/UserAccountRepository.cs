using TeraBankTask.DTO;
using TeraBankTask.Facade.Repositories;
using TeraBankTask.Repository;

namespace TeraBankTask.Repositories;

public class UserAccountRepository(TeraBankTaskDbContext context) : RepositoryBase<UserAccount>(context), IUserAccountRepository { }