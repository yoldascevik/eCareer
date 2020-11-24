using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Career.Repositories.Repository;
using Microsoft.EntityFrameworkCore;

namespace Career.EntityFramework.Repositories
{
    public class EfRepository<TContext, TEntity> : IRepository<TEntity> 
        where TContext : DbContext
        where TEntity : class
    {
        private readonly TContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public EfRepository(TContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Get()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public TEntity GetByKey(object key)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetByKeyAsync(object key)
        {
            throw new NotImplementedException();
        }

        public bool Any()
        {
            throw new NotImplementedException();
        }

        public bool Any(Expression<Func<TEntity, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AnyAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public long Count()
        {
            throw new NotImplementedException();
        }

        public long Count(Expression<Func<TEntity, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public async Task<long> CountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public TEntity First()
        {
            throw new NotImplementedException();
        }

        public TEntity First(Expression<Func<TEntity, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> FirstAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public TEntity FirstOrDefault()
        {
            throw new NotImplementedException();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> FirstOrDefaultAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public TEntity Single()
        {
            throw new NotImplementedException();
        }

        public TEntity Single(Expression<Func<TEntity, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> SingleAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public TEntity SingleOrDefault()
        {
            throw new NotImplementedException();
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> SingleOrDefaultAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public TEntity Add(TEntity item)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> AddAsync(TEntity item)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<TEntity> items)
        {
            throw new NotImplementedException();
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> items)
        {
            throw new NotImplementedException();
        }

        public TEntity Update(object key, TEntity item)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> UpdateAsync(object key, TEntity item)
        {
            throw new NotImplementedException();
        }

        public void Delete(object key)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<TEntity, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(object key)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> condition)
        {
            throw new NotImplementedException();
        }
    }
}