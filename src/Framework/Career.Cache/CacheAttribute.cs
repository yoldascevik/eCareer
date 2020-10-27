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
        private bool _dataReceivedFromCache;
        private readonly TimeSpan _duration;
        private readonly bool _slidingExpiration;
        private readonly string _cacheNamePrefix;
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
            string cacheKey = CacheHelper.GetCacheKey(args, _cacheNamePrefix);
            args.ReturnValue = _distributedCache.Get(cacheKey);
            _dataReceivedFromCache = args.ReturnValue != null;
        }

        public override async Task OnBeforeAsync(MethodExecutionArgs args)
        {
            string cacheKey = CacheHelper.GetCacheKey(args, _cacheNamePrefix);
            args.ReturnValue = await _distributedCache.GetAsync(cacheKey);
            _dataReceivedFromCache = args.ReturnValue != null;
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            if(_dataReceivedFromCache || args.ReturnValue == null)
                return;
            
            string cacheKey = CacheHelper.GetCacheKey(args, _cacheNamePrefix);
            _distributedCache.Set(cacheKey, _duration, _slidingExpiration, args.ReturnValue);
        }

        public override async Task OnSuccessAsync(MethodExecutionArgs args)
        {
            if(_dataReceivedFromCache || args.ReturnValue == null)
                return;
            
            string cacheKey = CacheHelper.GetCacheKey(args, _cacheNamePrefix);
            await _distributedCache.SetAsync(cacheKey, _duration, _slidingExpiration, args.ReturnValue);
        }

        public override AspectAttribute LoadDependencies(IServiceProvider serviceProvider)
        {
            _distributedCache ??= serviceProvider.GetRequiredService<ICareerDistributedCache>();
            if (_distributedCache == null)
                throw new ArgumentException("ICareerIDistributedCache is not registered on DI.");
            
            return base.LoadDependencies(serviceProvider);
        }
    }
}