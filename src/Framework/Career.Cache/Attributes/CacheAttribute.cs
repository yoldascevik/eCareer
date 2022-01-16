using AspectCore;
using AspectCore.Aspects;
using Career.Cache.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Career.Cache.Attributes;

[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method)]
public class CacheAttribute: AspectAttribute
{
    private const int DefaultTTL = 30 * TTLMultiplier.Minute;
        
    private bool _dataReceivedFromCache;
    private ILogger<CacheAttribute> _logger;
    private ICareerDistributedCache _distributedCache;
        
    public CacheAttribute()
    {
        TTL = DefaultTTL;
        SlidingExpiration = true;
    }

    /// <summary>
    /// Cache life time (seconds)
    /// </summary>
    public int TTL { get; set; }
  
    /// <summary>
    /// Automatically generated if not set
    /// </summary>
    public string CacheKey { get; set; }
        
    /// <summary>
    /// Refresh TTL when cache is used (default = true)
    /// </summary>
    public bool SlidingExpiration { get; set; }
        
    public override void OnBefore(MethodExecutionArgs args)
    {
        string cacheKey = CacheHelper.GetCacheKey(args, CacheKey);
        args.ReturnValue = _distributedCache.Get(cacheKey);
        _dataReceivedFromCache = args.ReturnValue != null;
            
        if(_dataReceivedFromCache)
            _logger.LogInformation("Data received from cache by key: {CacheKey}", cacheKey );
    }

    public override async Task OnBeforeAsync(MethodExecutionArgs args)
    {
        string cacheKey = CacheHelper.GetCacheKey(args, CacheKey);
        Type genericArgumentType = args.Method.ReturnType.GenericTypeArguments.First();
            
        args.ReturnValue = await _distributedCache.GetAsync(cacheKey, genericArgumentType);
        _dataReceivedFromCache = args.ReturnValue != null;
            
        if(_dataReceivedFromCache)
            _logger.LogInformation("Data received from cache by key: {CacheKey}", cacheKey );
    }

    public override void OnSuccess(MethodExecutionArgs args)
    {
        if(_dataReceivedFromCache || args.ReturnValue == null)
            return;
            
        string cacheKey = CacheHelper.GetCacheKey(args, CacheKey);
        _distributedCache.Set(cacheKey, TimeSpan.FromSeconds(TTL), SlidingExpiration, args.ReturnValue);
            
        _logger.LogInformation("Data cached with key: {CacheKey}", cacheKey );
    }

    public override async Task OnSuccessAsync(MethodExecutionArgs args)
    {
        if(_dataReceivedFromCache || args.ReturnValue == null)
            return;
            
        string cacheKey = CacheHelper.GetCacheKey(args, CacheKey);
        await _distributedCache.SetAsync(cacheKey, TimeSpan.FromSeconds(TTL), SlidingExpiration, args.ReturnValue);
            
        _logger.LogInformation("Data cached with key: {CacheKey}", cacheKey );
    }

    public override AspectAttribute LoadDependencies(IServiceProvider serviceProvider)
    {
        _distributedCache ??= serviceProvider.GetRequiredService<ICareerDistributedCache>();
        if (_distributedCache == null)
            throw new ArgumentException("ICareerIDistributedCache is not registered on DI.");
            
        _logger ??= serviceProvider.GetRequiredService<ILogger<CacheAttribute>>();
          
        return base.LoadDependencies(serviceProvider);
    }
}