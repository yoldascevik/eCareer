using Career.Mongo.Context;
using Career.Mongo.Repository.Contracts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Career.Shared.Interfaces;
using MongoDB.Bson;

namespace Career.Mongo.Repository
{
    public class MongoCommandRepository<T> : IMongoCommandRepository<T> where T : class
    {
        private IMongoCollection<T> Collection { get; }

        public MongoCommandRepository(IMongoContext context)
        {
            if (context == default)
                throw new ArgumentNullException(nameof(context));

            Collection = context.Database.GetCollection<T>(typeof(T).Name);
        }

        public virtual T Add(T item)
        {
            Collection.InsertOne(item);
            return item;
        }

        public virtual async Task<T> AddAsync(T item)
        { 
            await Collection.InsertOneAsync(item);
            return await Task.FromResult(item);
        }

        public virtual void AddRange(IEnumerable<T> items)
            => Collection.InsertMany(items);

        public virtual Task AddRangeAsync(IEnumerable<T> items)
            => Collection.InsertManyAsync(items);

        public virtual T Update(object key, T item) 
        {
            return Collection.FindOneAndReplace(
                FilterId(key),
                item,
                new FindOneAndReplaceOptions<T>
                {
                    IsUpsert = false,
                    ReturnDocument = ReturnDocument.After
                });
        }

        public virtual Task<T> UpdateAsync(object key, T item)
        {
            return Collection.FindOneAndReplaceAsync(
                FilterId(key),
                item,
                new FindOneAndReplaceOptions<T>
                {
                    IsUpsert = false,
                    ReturnDocument = ReturnDocument.After
                });
        }

        public virtual void Delete(object key)
        {
            if (typeof(ISoftDeletable).IsAssignableFrom(typeof(T)))
            {
                FilterDefinition<T> keyFilter = FilterId(key);
                T entity = Collection.Find(keyFilter).SingleOrDefault();
                if (entity != null)
                {
                    (entity as ISoftDeletable).IsDeleted = true;
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
                    (entity as ISoftDeletable).IsDeleted = true;
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
                    (entity as ISoftDeletable).IsDeleted = true;
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
                    (entity as ISoftDeletable).IsDeleted = true;
                    await Collection.ReplaceOneAsync(condition, entity);
                }
            }
            else
            {
                await Collection.DeleteOneAsync(condition);
            }
        }
        
        protected FilterDefinition<T> FilterId(object key)
            => Builders<T>.Filter.Eq("_id", ObjectId.Parse(key.ToString()));
    }
}