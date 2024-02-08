using TeraBankTask.DTO;
using TeraBankTask.Facade.Repositories;
using TeraBankTask.Repository;

namespace TeraBankTask.Repositories;

public class TransferRepository(TeraBankTaskDbContext context) : RepositoryBase<Transfer>(context), ITransferRepository { }
