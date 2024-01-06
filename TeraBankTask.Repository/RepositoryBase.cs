using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TeraBankTask.Facade.Repositories;
using TeraBankTask.Repository;

namespace TeraBankTask.Repositories;

public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
where TEntity : class
{
    protected readonly TeraBankTaskDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public RepositoryBase(TeraBankTaskDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context.Set<TEntity>();
    }

    public TEntity Get(params object[] id) => _dbSet.Find(id);

    public IQueryable<TEntity> Set(Expression<Func<TEntity, bool>> predicate) => _dbSet.Where(predicate).AsNoTracking();

    public IQueryable<TEntity> Set() => _dbSet;

    public TEntity Find(Expression<Func<TEntity, bool>> predicate)
    {
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));

        return _dbSet.SingleOrDefault(predicate)!;
    }

    public void Insert(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        _dbSet.Add(entity);
    }

    public void Update(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        _dbSet.Update(entity);
    }

    public void Delete(object id)
    {
        if (id is null) throw new ArgumentNullException(nameof(id));

        Delete(Get(id));
    }

    public void Delete(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        _dbSet.Remove(entity);
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _context.Set<TEntity>().ToList();
    }
}