

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using WebApp.Application.Abstractions.Services;
using WebApp.Persistence;

namespace WebApp.Infrastructure.Services.Caching;

public class CacheService : ICacheService
{
    private static ConcurrentDictionary<string, bool> CacheKeys = new();

    private readonly IDistributedCache _distributedCache;
    public CacheService (IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }
    public async Task<T?> GetAsync<T>(string cacheKey, CancellationToken cancellationToken = default)
        where T : class
    {
        string? cachedValue = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);

        if (cachedValue == null)
        {
            return null;
        }

        T? value = JsonConvert.DeserializeObject<T>(
           cachedValue,
           new JsonSerializerSettings
           {
               ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
               ContractResolver = new PrivateResolver()
           });


        return value;
    }

    public async Task<T?> GetAsync<T>(string cacheKey, Func<Task<T>> factory, CancellationToken cancellationToken = default)
        where T : class
    {
        T? cachedValue = await GetAsync<T>(cacheKey, cancellationToken);

        if (cachedValue is not null)
        {
            return cachedValue;
        }

        cachedValue = await factory();

        if (cachedValue is null) 
        {
            return null;
        }

        await SetAsync(cacheKey, cachedValue, cancellationToken);

        return cachedValue;

    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await _distributedCache.RemoveAsync(key, cancellationToken);

        CacheKeys.TryRemove(key, out bool _);
    }

    public async Task RemoveByPrefixAsync(string prefix, CancellationToken cancellationToken = default)
    {
        IEnumerable<Task> tasks = CacheKeys
            .Keys
            .Where(k => k.StartsWith(prefix))
            .Select(k => RemoveAsync(k, cancellationToken));

        await Task.WhenAll(tasks);
    }

    public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
    {
        string cachedValue = JsonConvert.SerializeObject(value);

        await _distributedCache.SetStringAsync(key, cachedValue, cancellationToken);

        CacheKeys.TryAdd(key, true);
    }
}
