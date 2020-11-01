using System;
using System.Threading.Tasks;

namespace Career.Cache
{
    public interface ICareerDistributedCache
    {
        object Get(string cacheKey);
        object Get(string cacheKey, Type deserializeType);
 
        Task<object> GetAsync(string cacheKey);
        Task<object> GetAsync(string cacheKey, Type deserializeType);
        
        T Get<T>(string cacheKey);
        Task<T> GetAsync<T>(string cacheKey);
        
        void Set(string cacheKey, TimeSpan duration, bool slidingExpiration, object data);
        Task SetAsync(string cacheKey, TimeSpan duration, bool slidingExpiration, object data);

        void Refresh(string cacheKey);
        Task RefreshAsync(string cacheKey);
        
        void Remove(string cacheKey);
        Task RemoveAsync(string cacheKey);
        
        void RemoveByPattern(string cachePattern);
        Task RemoveByPatternAsync(string cachePattern);
        
        void Clear();
        Task ClearAsync();
    }
}