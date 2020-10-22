using System;
using System.Threading.Tasks;
using AspectCore;
using AspectCore.Aspects;
using Microsoft.Extensions.DependencyInjection;

namespace Career.Cache
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheAttribute: AspectAttribute
    {
        private readonly string _cacheNamePrefix;
        private readonly bool _slidingExpiration;
        private readonly TimeSpan _duration;
        private ICareerDistributedCache _distributedCache;
        
        public CacheAttribute()
        {
            _slidingExpiration = false;
            _duration = TimeSpan.FromMinutes(30);
        }
        
        public CacheAttribute(TimeSpan duration, bool slidingExpiration = false)
        {
            _duration = duration;
            _slidingExpiration = slidingExpiration;
        }
        
        public CacheAttribute(string cacheNamePrefix) 
            : this()
        {
            if (string.IsNullOrEmpty(cacheNamePrefix))
                throw new ArgumentNullException(nameof(cacheNamePrefix));
            
            _cacheNamePrefix = cacheNamePrefix;
        }
        
        public CacheAttribute(string cacheNamePrefix, TimeSpan duration, bool slidingExpiration = false)
            : this(cacheNamePrefix)
        {
            _duration = duration;
            _slidingExpiration = slidingExpiration;
        }
        
        public override void OnBefore(MethodExecutionArgs args)
        {
            _distributedCache = CreateDistributedCacheInstance(args.ServiceProvider);
            
            string cacheKey = CacheHelper.GetCacheKey(args, _cacheNamePrefix);
            args.ReturnValue = _distributedCache.Get(cacheKey); 
        }

        public override async Task OnBeforeAsync(MethodExecutionArgs args)
        {
            _distributedCache = CreateDistributedCacheInstance(args.ServiceProvider);
            
            string cacheKey = CacheHelper.GetCacheKey(args, _cacheNamePrefix);
            args.ReturnValue = await _distributedCache.GetAsync(cacheKey); 
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            _distributedCache = CreateDistributedCacheInstance(args.ServiceProvider);
            
            string cacheKey = CacheHelper.GetCacheKey(args, _cacheNamePrefix);
            if (args.ReturnValue != null)
                _distributedCache.Set(cacheKey, _duration, _slidingExpiration, args.ReturnValue);
        }

        public override async Task OnSuccessAsync(MethodExecutionArgs args)
        {
            _distributedCache = CreateDistributedCacheInstance(args.ServiceProvider);
            
            string cacheKey = CacheHelper.GetCacheKey(args, _cacheNamePrefix);
            if (args.ReturnValue != null)
                await _distributedCache.SetAsync(cacheKey, _duration, _slidingExpiration, args.ReturnValue);
        }

        private ICareerDistributedCache CreateDistributedCacheInstance(IServiceProvider serviceProvider)
        {
            ICareerDistributedCache instance =  _distributedCache ?? serviceProvider.GetService<ICareerDistributedCache>();
            if (instance == null)
                throw new ArgumentException("ICareerIDistributedCache is not registered on DI.", nameof(instance));

            return instance;
        }
    }
}