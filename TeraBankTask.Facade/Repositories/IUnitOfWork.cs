namespace TeraBankTask.Facade.Repositories;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IUserAccountRepository UserAccountRepository { get; }

    void BeginTransaction();
    void Commit();
    void Rollback();
    void SaveChanges();
    Task SaveChangesAsync();
}