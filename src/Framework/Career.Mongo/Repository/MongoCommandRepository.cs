using Career.Mongo.Context;
using Career.Mongo.Repository.Contracts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Career.Domain;
using Career.Domain.DomainEvent.Dispatcher;
using Career.Domain.Entities;
using Career.Exceptions;
using MongoDB.Bson;

namespace Career.Mongo.Repository
{
    public class MongoCommandRepository<T> : IMongoCommandRepository<T> where T : class
    {
        private IMongoCollection<T> Collection { get; }
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public MongoCommandRepository(IMongoContext context, IDomainEventDispatcher domainEventDispatcher)
        {
            Check.NotNull(context, nameof(context));

            _domainEventDispatcher = domainEventDispatcher;
            Collection = context.Database.GetCollection<T>(typeof(T).Name);
        }

        public virtual T Add(T item)
        {
            Collection.InsertOne(item);
            DispatchDomainEvents(item).Wait();
            
            return item;
        }

        public virtual async Task<T> AddAsync(T item)
        { 
            await Collection.InsertOneAsync(item);
            await DispatchDomainEvents(item);
            
            return await Task.FromResult(item);
        }

        public virtual void AddRange(IEnumerable<T> items)
        {
            Collection.InsertMany(items);
            DispatchDomainEvents(items).Wait();
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> items)
        {
            await Collection.InsertManyAsync(items);
            await DispatchDomainEvents(items);
        }

        public virtual T Update(object key, T item) 
        {
            var result = Collection.FindOneAndReplace(
                FilterId(key),
                item,
                new FindOneAndReplaceOptions<T>
                {
                    IsUpsert = false,
                    ReturnDocument = ReturnDocument.After
                });
            
            DispatchDomainEvents(item).Wait();
            return result;
        }

        public virtual async Task<T> UpdateAsync(object key, T item)
        {
            var result = await Collection.FindOneAndReplaceAsync(
                FilterId(key),
                item,
                new FindOneAndReplaceOptions<T>
                {
                    IsUpsert = false,
                    ReturnDocument = ReturnDocument.After
                });
            
            DispatchDomainEvents(item).Wait();
            return result;
        }

        public virtual void Delete(object key)
        {
            if (typeof(ISoftDeletable).IsAssignableFrom(typeof(T)))
            {
                FilterDefinition<T> keyFilter = FilterId(key);
                T entity = Collection.Find(keyFilter).SingleOrDefault();
                if (entity != null)
                {
                    ((ISoftDeletable) entity).IsDeleted = true;
                    Collection.ReplaceOne(keyFilter, entity);
                }
            }
            else
            {
                Collection.DeleteOne(FilterId(key));
            }
        }

        public virtual async Task DeleteAsync(object key)
        {
            FilterDefinition<T> keyFilter = FilterId(key);
            if (typeof(ISoftDeletable).IsAssignableFrom(typeof(T)))
            {
                T entity = await Collection.Find(keyFilter).SingleOrDefaultAsync();
                if (entity != null)
                {
                    ((ISoftDeletable) entity).IsDeleted = true;
                    await Collection.ReplaceOneAsync(keyFilter, entity);
                }
            }
            else
            {
                await Collection.DeleteOneAsync(FilterId(key));
            }
        }

        public virtual void Delete(Expression<Func<T, bool>> condition)
        {
            if (typeof(ISoftDeletable).IsAssignableFrom(typeof(T)))
            {
                T entity = Collection.Find(condition).FirstOrDefault();
                if (entity != null)
                {
                    ((ISoftDeletable) entity).IsDeleted = true;
                    Collection.ReplaceOne(condition, entity);
                }
            }
            else
            {
                Collection.DeleteOne(condition);
            }
        }

        public virtual async Task DeleteAsync(Expression<Func<T, bool>> condition)
        {
            if (typeof(ISoftDeletable).IsAssignableFrom(typeof(T)))
            {
                T entity = await Collection.Find(condition).FirstOrDefaultAsync();
                if (entity != null)
                {
                    ((ISoftDeletable) entity).IsDeleted = true;
                    await Collection.ReplaceOneAsync(condition, entity);
                }
            }
            else
            {
                await Collection.DeleteOneAsync(condition);
            }
        }

        protected FilterDefinition<T> FilterId(object key)
        {
            if (key is Guid guidKey)
            {
                return Builders<T>.Filter.Eq(new StringFieldDefinition<T, Guid>("_id"), guidKey);
            }
            
            return Builders<T>.Filter.Eq("_id", ObjectId.Parse(key.ToString()));
        }
        
        #region Private Helpers Of DomainEvent

        private async Task DispatchDomainEvents(T entity)
        {
            if (_domainEventDispatcher == null || entity == null) 
                return;
            
            if (entity is DomainEntity domainEntity 
                && domainEntity.DomainEvents != null 
                && domainEntity.DomainEvents.Any())
            {
                await _domainEventDispatcher.Dispatch(domainEntity.DomainEvents);
                domainEntity.ClearDomainEvents();
            }
        }
        
        private async Task DispatchDomainEvents(IEnumerable<T> entities)
        {
            if (_domainEventDispatcher == null || entities == null) 
                return;
            
            IEnumerable<DomainEntity> domainEntities = entities
                .Where(e => e is DomainEntity)
                .Cast<DomainEntity>();
            
            foreach (DomainEntity domainEntity in domainEntities)
            {
                if (domainEntity.DomainEvents != null && domainEntity.DomainEvents.Any())
                {
                    await _domainEventDispatcher.Dispatch(domainEntity.DomainEvents);
                    domainEntity.ClearDomainEvents();
                }
            }
        }

        #endregion
    }
}