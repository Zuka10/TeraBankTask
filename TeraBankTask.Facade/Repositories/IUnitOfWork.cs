namespace TeraBankTask.Facade.Repositories;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IUserAccountRepository UserAccountRepository { get; }
    IDepositRepository DepositRepository { get; }
    IWithdrawRepository WithdrawRepository { get; }
    ITransferRepository TransferRepository { get; }

    void BeginTransaction();
    void Commit();
    void Rollback();
    void SaveChanges();
    Task SaveChangesAsync();
}