using System.Linq.Expressions;

namespace TeraBankTask.Facade.Repositories;

public interface IRepositoryBase<TEntity>
{
    TEntity Get(params object[] id);
    IQueryable<TEntity> Set(Expression<Func<TEntity, bool>> predicate);
    IEnumerable<TEntity> GetAll();
    IQueryable<TEntity> Set();
    TEntity Find(Expression<Func<TEntity, bool>> predicate);
    void Insert(TEntity entity);
    void Update(TEntity entity);
    void Delete(object id);
    void Delete(TEntity entity);
}