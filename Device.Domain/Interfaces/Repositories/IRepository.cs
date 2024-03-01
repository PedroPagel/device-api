using Device.Domain.Entities;
using System.Linq.Expressions;

namespace Device.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Add<T>(TEntity entity) where T : TEntity;
        Task<int> Delete(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> Update<T>(TEntity entity) where T : TEntity;
    }
}
