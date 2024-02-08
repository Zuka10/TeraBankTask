using TeraBankTask.DTO;
using TeraBankTask.Facade.Repositories;
using TeraBankTask.Repository;

namespace TeraBankTask.Repositories;

public class DepositRepository(TeraBankTaskDbContext context) : RepositoryBase<Deposit>(context), IDepositRepository { }
