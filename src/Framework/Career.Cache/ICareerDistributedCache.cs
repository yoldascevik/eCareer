using System;
using System.Threading.Tasks;

namespace Career.Cache
{
    public interface ICareerDistributedCache
    {
        object Get(string cacheKey);
        Task<object> GetAsync(string cacheKey);
        
        T Get<T>(string cacheKey);
        Task<T> GetAsync<T>(string cacheKey);

        void Refresh(string cacheKey);
        Task RefreshAsync(string cacheKey);
        
        void Remove(string cacheKey);
        Task RemoveAsync(string cacheKey);
        
        void Set(string cacheKey, TimeSpan duration, bool slidingExpiration, object data);
        Task SetAsync(string cacheKey, TimeSpan duration, bool slidingExpiration, object data);
    }
}