using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using TeraBankTask.Facade.Repositories;
using TeraBankTask.Repository;

namespace TeraBankTask.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    private readonly TeraBankTaskDbContext _context;
    private readonly ILogger<UnitOfWork> _logger;
    private readonly Lazy<IUserRepository> _userRepository;
    private readonly Lazy<IUserAccountRepository> _userAccountRepository;

    public UnitOfWork(TeraBankTaskDbContext context, ILogger<UnitOfWork> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userRepository = new Lazy<IUserRepository>(() => new UserRepository(context));
        _userAccountRepository = new Lazy<IUserAccountRepository>(() => new UserAccountRepository(context));
    }

    public IUserRepository UserRepository => _userRepository.Value;
    public IUserAccountRepository UserAccountRepository => _userAccountRepository.Value;

    public void BeginTransaction()
    {
        try
        {
            _transaction = _context.Database.BeginTransaction();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to begin transaction");
            throw;
        }
    }

    public void Commit()
    {
        try
        {
            _transaction?.Commit();
            _transaction?.Dispose();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to commit transaction");
            throw;
        }
    }

    public void Rollback()
    {
        _transaction?.Rollback();
        _transaction?.Dispose();
    }

    public void SaveChanges()
    {
        try
        {
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DbContext error");
            throw;
        }

    }

    public void Dispose()
    {
        try
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to rollback transaction");
            throw;
        }
    }

    public async Task SaveChangesAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DbContext error");
            throw;
        }
    }
}