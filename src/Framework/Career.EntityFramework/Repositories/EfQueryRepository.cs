using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Career.Repositories.Repository;
using Microsoft.EntityFrameworkCore;

namespace Career.EntityFramework.Repositories
{
    public class EfQueryRepository<TContext, TEntity> : IQueryRepository<TEntity> 
        where TContext : DbContext
        where TEntity : class
    {
        private readonly TContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public EfQueryRepository(TContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Get()
            => _dbSet.AsNoTracking();

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> condition)
            => _dbSet.AsNoTracking().Where(condition);

        public async Task<IEnumerable<TEntity>> GetAsync()
            => await _dbSet.AsNoTracking().ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> condition)
            => await _dbSet.AsNoTracking().Where(condition).ToListAsync();

        public TEntity GetByKey(object key)
            => _dbSet.Find(key);

        public async Task<TEntity> GetByKeyAsync(object key)
            => await _dbSet.FindAsync(key);

        public bool Any()
            => _dbSet.AsNoTracking().Any();

        public bool Any(Expression<Func<TEntity, bool>> condition)
            => _dbSet.AsNoTracking().Any(condition);

        public async Task<bool> AnyAsync()
            => await _dbSet.AsNoTracking().AnyAsync();

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> condition)
            => await _dbSet.AsNoTracking().AnyAsync(condition);

        public long Count()
            => _dbSet.AsNoTracking().Count();

        public long Count(Expression<Func<TEntity, bool>> condition)
            => _dbSet.AsNoTracking().Count(condition);

        public async Task<long> CountAsync()
            => await _dbSet.AsNoTracking().CountAsync();

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> condition)
            => await _dbSet.AsNoTracking().CountAsync(condition);

        public TEntity First()
            => _dbSet.AsNoTracking().First();

        public TEntity First(Expression<Func<TEntity, bool>> condition)
            => _dbSet.AsNoTracking().First(condition);

        public async Task<TEntity> FirstAsync()
            => await _dbSet.AsNoTracking().FirstAsync();

        public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> condition)
            => await _dbSet.AsNoTracking().FirstAsync(condition);

        public TEntity FirstOrDefault()
            => _dbSet.AsNoTracking().FirstOrDefault();

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> condition)
            => _dbSet.AsNoTracking().FirstOrDefault(condition);

        public async Task<TEntity> FirstOrDefaultAsync()
            => await _dbSet.AsNoTracking().FirstOrDefaultAsync();

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> condition)
            => await _dbSet.AsNoTracking().FirstOrDefaultAsync(condition);

        public TEntity Single()
            => _dbSet.AsNoTracking().Single();

        public TEntity Single(Expression<Func<TEntity, bool>> condition)
            => _dbSet.AsNoTracking().Single(condition);

        public async Task<TEntity> SingleAsync()
            => await _dbSet.AsNoTracking().SingleAsync();

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> condition)
            => await _dbSet.AsNoTracking().SingleAsync(condition);

        public TEntity SingleOrDefault()
            => _dbSet.AsNoTracking().SingleOrDefault();

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> condition)
            => _dbSet.AsNoTracking().SingleOrDefault();

        public async Task<TEntity> SingleOrDefaultAsync()
            => await _dbSet.AsNoTracking().SingleOrDefaultAsync();
        
        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> condition)
            => await _dbSet.AsNoTracking().SingleOrDefaultAsync(condition);
    }
}