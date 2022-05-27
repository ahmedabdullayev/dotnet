using Base.Contracts.Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories
{
    public class BaseRepository<TEntity, TAppDbContext>
        where TEntity: class, IDomainEntityId
        where TAppDbContext: DbContext
    {
        protected readonly TAppDbContext RepoDbContext;
        protected readonly DbSet<TEntity> RepoDbSet;

        public BaseRepository(TAppDbContext dbContext)
        {
            RepoDbContext = dbContext;
            RepoDbSet = dbContext.Set<TEntity>();

        }
        
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(bool noTracking = true)
        {
            if (noTracking)
            {
                return await RepoDbSet.AsNoTracking().ToListAsync();
            }
            return await RepoDbSet.ToListAsync();
        }
        
        public virtual async Task<TEntity?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();
            
            if (noTracking)
            {
                query = query.AsNoTracking();
            }
            
            return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public virtual TEntity Add(TEntity entity)
        {
            return RepoDbSet.Add(entity).Entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            return RepoDbSet.Update(entity).Entity;
        }

        public virtual TEntity Remove(TEntity entity)
        {
            return RepoDbSet.Remove(entity).Entity;        
        }

        public virtual async Task<TEntity> RemoveAsync(Guid id)
        {
            var entity = await FirstOrDefaultAsync(id);
            return Remove(entity!);
        }

        public virtual async Task<bool> ExistsAsync(Guid id)
        {
            return await RepoDbSet.AnyAsync(e => e.Id.Equals(id));
        }


    }
}