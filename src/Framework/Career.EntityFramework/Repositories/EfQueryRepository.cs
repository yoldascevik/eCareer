using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Career.Repositories.Repository;
using Microsoft.EntityFrameworkCore;

namespace Career.EntityFramework.Repositories
{
    /// <summary>
    /// Warning: All methods call with AsNoTracking
    /// </summary>
    public class EfQueryRepository<TContext, TEntity> : IQueryRepository<TEntity> 
        where TContext : DbContext
        where TEntity : class
    {
        protected readonly TContext DbContext;
        protected readonly DbSet<TEntity> DbSet;

        public EfQueryRepository(TContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Get()
            => DbSet.AsNoTracking();

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> condition)
            => DbSet.AsNoTracking().Where(condition);

        public async Task<IEnumerable<TEntity>> GetAsync()
            => await DbSet.AsNoTracking().ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> condition)
            => await DbSet.AsNoTracking().Where(condition).ToListAsync();

        public TEntity GetByKey(object key)
            => DbSet.Find(key);

        public async Task<TEntity> GetByKeyAsync(object key)
            => await DbSet.FindAsync(key);

        public bool Any()
            => DbSet.AsNoTracking().Any();

        public bool Any(Expression<Func<TEntity, bool>> condition)
            => DbSet.AsNoTracking().Any(condition);

        public async Task<bool> AnyAsync()
            => await DbSet.AsNoTracking().AnyAsync();

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> condition)
            => await DbSet.AsNoTracking().AnyAsync(condition);

        public long Count()
            => DbSet.AsNoTracking().Count();

        public long Count(Expression<Func<TEntity, bool>> condition)
            => DbSet.AsNoTracking().Count(condition);

        public async Task<long> CountAsync()
            => await DbSet.AsNoTracking().CountAsync();

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> condition)
            => await DbSet.AsNoTracking().CountAsync(condition);

        public TEntity First()
            => DbSet.AsNoTracking().First();

        public TEntity First(Expression<Func<TEntity, bool>> condition)
            => DbSet.AsNoTracking().First(condition);

        public async Task<TEntity> FirstAsync()
            => await DbSet.AsNoTracking().FirstAsync();

        public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> condition)
            => await DbSet.AsNoTracking().FirstAsync(condition);

        public TEntity FirstOrDefault()
            => DbSet.AsNoTracking().FirstOrDefault();

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> condition)
            => DbSet.AsNoTracking().FirstOrDefault(condition);

        public async Task<TEntity> FirstOrDefaultAsync()
            => await DbSet.AsNoTracking().FirstOrDefaultAsync();

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> condition)
            => await DbSet.AsNoTracking().FirstOrDefaultAsync(condition);

        public TEntity Single()
            => DbSet.AsNoTracking().Single();

        public TEntity Single(Expression<Func<TEntity, bool>> condition)
            => DbSet.AsNoTracking().Single(condition);

        public async Task<TEntity> SingleAsync()
            => await DbSet.AsNoTracking().SingleAsync();

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> condition)
            => await DbSet.AsNoTracking().SingleAsync(condition);

        public TEntity SingleOrDefault()
            => DbSet.AsNoTracking().SingleOrDefault();

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> condition)
            => DbSet.AsNoTracking().SingleOrDefault();

        public async Task<TEntity> SingleOrDefaultAsync()
            => await DbSet.AsNoTracking().SingleOrDefaultAsync();
        
        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> condition)
            => await DbSet.AsNoTracking().SingleOrDefaultAsync(condition);
    }
}