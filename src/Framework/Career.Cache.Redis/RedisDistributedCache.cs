using System;
using System.Threading.Tasks;
using MessagePack;
using Microsoft.Extensions.Caching.Distributed;

namespace Career.Cache.Redis
{
    public class RedisDistributedCache : ICareerDistributedCache
    {
        private readonly IDistributedCache _distributedCache;

        public RedisDistributedCache(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        }

        public object Get(string cacheKey)
            => Get<object>(cacheKey);

        public async Task<object> GetAsync(string cacheKey)
            => GetAsync<object>(cacheKey);
       
        public T Get<T>(string cacheKey)
        {
            if(string.IsNullOrEmpty(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));
            
            byte[] cacheData = _distributedCache.Get(cacheKey);
            if (cacheData.Length == 0)
                return default(T);
            
            return MessagePackSerializer.Deserialize<T>(cacheData);
        }

        public async Task<T> GetAsync<T>(string cacheKey)
        {
            if(string.IsNullOrEmpty(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));
            
            byte[] cacheData = await _distributedCache.GetAsync(cacheKey);
            if (cacheData.Length == 0)
                return default(T);
            
            return MessagePackSerializer.Deserialize<T>(cacheData);
        }
        
        public void Set(string cacheKey, TimeSpan duration, bool slidingExpiration, object data)
        {
            if(string.IsNullOrEmpty(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            if (data == null)
                throw new ArgumentNullException(nameof(data));

            byte[] cacheData = MessagePackSerializer.Serialize(data);
            
            _distributedCache.Set(cacheKey, cacheData, new DistributedCacheEntryOptions()
            {
                SlidingExpiration = slidingExpiration ? duration : (TimeSpan?)null,
                AbsoluteExpiration = !slidingExpiration ? DateTimeOffset.Now + duration : (DateTimeOffset?)null
            });
        }

        public async Task SetAsync(string cacheKey, TimeSpan duration, bool slidingExpiration, object data)
        {
            if(string.IsNullOrEmpty(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            if (data == null)
                throw new ArgumentNullException(nameof(data));

            byte[] cacheData = MessagePackSerializer.Serialize(data);
            
            await _distributedCache.SetAsync(cacheKey, cacheData, new DistributedCacheEntryOptions()
            {
                SlidingExpiration = slidingExpiration ? duration : (TimeSpan?)null,
                AbsoluteExpiration = !slidingExpiration ? DateTimeOffset.Now + duration : (DateTimeOffset?)null
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
    }
}