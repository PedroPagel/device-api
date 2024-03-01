using Device.Domain.Entities;
using Device.Domain.Interfaces.Repositories;
using Device.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Device.Infrastructure.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        public readonly DbSet<TEntity> DbSet;
        public DeviceContext Db;

        protected Repository(DeviceContext db)
        {
            DbSet = db.Set<TEntity>();
            Db = db;
        }

        public void Dispose() => Db?.Dispose();

        public virtual async Task<IEnumerable<TEntity>> GetAll() =>
            await DbSet.AsNoTracking().ToListAsync();

        public virtual async Task<TEntity> Add<T>(TEntity entity) where T : TEntity
        {
            await DbSet.AddAsync(entity);

            if (await Db.SaveChangesAsync() > 0)
            {
                return entity;
            }

            return null;
        }

        public virtual async Task<TEntity> Update<T>(TEntity entity) where T : TEntity
        {
            DbSet.Update(entity);

            if (await Db.SaveChangesAsync() > 0)
            {
                return entity;
            }

            return null;
        }

        public virtual async Task<int> Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var range = DbSet.Where(predicate);
            DbSet.RemoveRange(range);
            return await Db.SaveChangesAsync();
        }
    }
}
