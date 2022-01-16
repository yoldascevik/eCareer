using System;
using System.Threading.Tasks;
using AspectCore;
using AspectCore.Aspects;
using Career.Cache.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Career.Cache.Attributes;

[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = true)]
public class CacheInvalidateAttribute: AspectAttribute
{
    private readonly string _cacheKey;
    private readonly Type _targetType;
    private readonly string _targetMethodName;
        
    private ILogger<CacheInvalidateAttribute> _logger;
    private ICareerDistributedCache _distributedCache;

    #region Ctors

    public CacheInvalidateAttribute()
    {
            
    }
        
    public CacheInvalidateAttribute(string cacheKey)
    {
        if (string.IsNullOrEmpty(cacheKey))
            throw new ArgumentNullException(nameof(cacheKey));

        _cacheKey = cacheKey;
    }
        
    public CacheInvalidateAttribute(Type targetType)
    {
        if (targetType == null)
            throw new ArgumentNullException(nameof(targetType));

        _targetType = targetType;
    }

    public CacheInvalidateAttribute(Type targetType, string targetMethodName)
        : this(targetType)
    {
        if (string.IsNullOrEmpty(targetMethodName))
            throw new ArgumentNullException(nameof(targetMethodName));

        _targetMethodName = targetMethodName;
    }

    #endregion
        
    public override void OnSuccess(MethodExecutionArgs args)
    {
        string cacheKey = GetCacheName(args);
        _distributedCache.RemoveByPattern(cacheKey);
            
        _logger.LogInformation("Cache invalidated for key: {CacheKey} after invoked method : {MethodName}", cacheKey, args.Method.Name);
    }

    public override async Task OnSuccessAsync(MethodExecutionArgs args)
    {
        string cacheKey = GetCacheName(args);
        await _distributedCache.RemoveByPatternAsync(cacheKey);
            
        _logger.LogInformation("Cache invalidated for key: {CacheKey} after invoked method : {MethodName}", cacheKey, args.Method.Name);
    }

    public override AspectAttribute LoadDependencies(IServiceProvider serviceProvider)
    {
        _distributedCache ??= serviceProvider.GetRequiredService<ICareerDistributedCache>();
        if (_distributedCache == null)
            throw new ArgumentException("ICareerIDistributedCache is not registered on DI.");
            
        _logger ??= serviceProvider.GetRequiredService<ILogger<CacheInvalidateAttribute>>();
          
        return base.LoadDependencies(serviceProvider);
    }

    private string GetCacheName(MethodExecutionArgs args)
    {
        if (!string.IsNullOrEmpty(_cacheKey))
            return CacheHelper.GetCacheKey(_cacheKey);

        if (_targetType != null)
            return CacheHelper.GetCacheKey(_targetType, _targetMethodName);

        return CacheHelper.GetCacheKey(args.Method.DeclaringType);
    }
}