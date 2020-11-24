using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Career.Repositories.Repository;
using Microsoft.EntityFrameworkCore;

namespace Career.EntityFramework.Repositories
{
    public class EfCommandRepository<TContext, TEntity> : ICommandRepository<TEntity>
        where TContext : DbContext
        where TEntity : class
    {
        private readonly TContext _dbContext;

        public EfCommandRepository(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TEntity Add(TEntity item)
            => _dbContext.Add(item).Entity;
        
        public async Task<TEntity> AddAsync(TEntity item)
            => (await _dbContext.AddAsync(item)).Entity;

        public void AddRange(IEnumerable<TEntity> items)
            => _dbContext.AddRange(items);

        public async Task AddRangeAsync(IEnumerable<TEntity> items)
            => await _dbContext.AddRangeAsync(items);

        public TEntity Update(object key, TEntity item)
            => throw new NotImplementedException();

        public async Task<TEntity> UpdateAsync(object key, TEntity item)
            => throw new NotImplementedException();

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