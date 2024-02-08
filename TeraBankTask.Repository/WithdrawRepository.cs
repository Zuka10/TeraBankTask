using TeraBankTask.DTO;
using TeraBankTask.Facade.Repositories;
using TeraBankTask.Repository;

namespace TeraBankTask.Repositories;

public class WithdrawRepositry(TeraBankTaskDbContext context) : RepositoryBase<Withdraw>(context), IWithdrawRepository { }