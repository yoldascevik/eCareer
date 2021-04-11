using System;
using System.Linq;
using System.Threading.Tasks;
using MessagePack;
using MessagePack.Resolvers;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Career.Cache.Redis
{
    public class RedisDistributedCache : ICareerDistributedCache
    {
        private readonly IDatabase _cache;
        private readonly IDistributedCache _distributedCache;
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly string _instanceName;

        public RedisDistributedCache(
            IDistributedCache distributedCache, 
            IConnectionMultiplexer connectionMultiplexer, 
            IOptions<RedisCacheOptions> redisCacheOptions)
        {
            _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
            _connectionMultiplexer = connectionMultiplexer ?? throw new ArgumentNullException(nameof(connectionMultiplexer));
            _instanceName = redisCacheOptions.Value.InstanceName ?? string.Empty;
            _cache = connectionMultiplexer.GetDatabase();
        }
        
        public object Get(string cacheKey) 
            => Get(cacheKey, typeof(object));

        public object Get(string cacheKey, Type deserializeType)
        {
            if(string.IsNullOrEmpty(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            if (deserializeType == null)
                throw new ArgumentNullException(nameof(deserializeType));
            
            byte[] cacheData = _distributedCache.Get(cacheKey);
            if (cacheData == null)
                return null;
            
            return MessagePackSerializer.Deserialize(deserializeType, cacheData, ContractlessStandardResolver.Options);
        }

        public async Task<object> GetAsync(string cacheKey) 
            => await GetAsync(cacheKey, typeof(object));

        public async Task<object> GetAsync(string cacheKey, Type deserializeType)
        {
            if(string.IsNullOrEmpty(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));
            
            if (deserializeType == null)
                throw new ArgumentNullException(nameof(deserializeType));
            
            byte[] cacheData = await _distributedCache.GetAsync(cacheKey);
            if (cacheData == null)
                return null;
            
            return MessagePackSerializer.Deserialize(deserializeType, cacheData, ContractlessStandardResolver.Options);
        }

        public T Get<T>(string cacheKey) 
            => (T) Get(cacheKey, typeof(T));

        public async Task<T> GetAsync<T>(string cacheKey)
            => await (GetAsync(cacheKey, typeof(T)) as Task<T>);
        
        public void Set(string cacheKey, TimeSpan duration, bool slidingExpiration, object data)
        {
            if(string.IsNullOrEmpty(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            if (data == null)
                throw new ArgumentNullException(nameof(data));

            byte[] cacheData = MessagePackSerializer.Serialize(data, ContractlessStandardResolver.Options);
            
            _distributedCache.Set(cacheKey, cacheData, new DistributedCacheEntryOptions()
            {
                SlidingExpiration = slidingExpiration ? duration : null,
                AbsoluteExpiration = !slidingExpiration ? DateTimeOffset.Now + duration : null
            });
        }

        public async Task SetAsync(string cacheKey, TimeSpan duration, bool slidingExpiration, object data)
        {
            if(string.IsNullOrEmpty(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            if (data == null)
                throw new ArgumentNullException(nameof(data));
            
            byte[] cacheData = MessagePackSerializer.Serialize(data, ContractlessStandardResolver.Options);
            
            await _distributedCache.SetAsync(cacheKey, cacheData, new DistributedCacheEntryOptions()
            {
                SlidingExpiration = slidingExpiration ? duration : null,
                AbsoluteExpiration = !slidingExpiration ? DateTimeOffset.Now + duration : null
            });
        }

        public void Refresh(string cacheKey)
        {
            if(string.IsNullOrEmpty(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            _distributedCache.Refresh(cacheKey);
        }

        public async Task RefreshAsync(string cacheKey)
        {
            if(string.IsNullOrEmpty(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            await _distributedCache.RefreshAsync(cacheKey);
        }

        public void Remove(string cacheKey)
        {
            if(string.IsNullOrEmpty(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            _distributedCache.Remove(cacheKey);
        }

        public async Task RemoveAsync(string cacheKey)
        {
            if(string.IsNullOrEmpty(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            await _distributedCache.RemoveAsync(cacheKey);
        }

        public void RemoveByPattern(string cachePattern)
        {
            foreach (var endpoint in _connectionMultiplexer.GetEndPoints(true))
            {
                var server = _connectionMultiplexer.GetServer(endpoint);
                var keys = server.Keys(database: _cache.Database, pattern: _instanceName + cachePattern).ToArray();
                _cache.KeyDeleteAsync(keys);
            }
        }

        public async Task RemoveByPatternAsync(string cachePattern)
        {
            foreach (var endpoint in _connectionMultiplexer.GetEndPoints(true))
            {
                var server = _connectionMultiplexer.GetServer(endpoint);
                var keys = server.Keys(database: _cache.Database, pattern: _instanceName + cachePattern).ToArray();
                await _cache.KeyDeleteAsync(keys);
            }
        }

        public void Clear()
        {
            foreach (var endpoint in _connectionMultiplexer.GetEndPoints(true))
            {
                _connectionMultiplexer.GetServer(endpoint).FlushDatabase(_cache.Database);
            }
        }

        public async Task ClearAsync()
        {
            foreach (var endpoint in _connectionMultiplexer.GetEndPoints(true))
            {
               await _connectionMultiplexer.GetServer(endpoint).FlushDatabaseAsync(_cache.Database);
            }
        }
    }
}