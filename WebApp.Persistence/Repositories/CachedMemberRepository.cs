
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using WebApp.Domain.Entities;
using WebApp.Domain.QueryObjects;
using WebApp.Domain.Repositories;
using WebApp.Domain.ValueObjects;

namespace WebApp.Persistence.Repositories;

public class CachedMemberRepository : IMemberRepository
{
    private readonly MemberRepository _decorator;
    private readonly IDistributedCache _distributedCache;
    private readonly DataContext _dbContext;
    public CachedMemberRepository(MemberRepository decorator, IDistributedCache distributedCache, DataContext dataContext)
    {
        _distributedCache = distributedCache;
        _dbContext = dataContext;
        _decorator = decorator;
    }
    public void Add(Member member) => _decorator.Add(member);

    public Task<List<Member>> GetAllMembersAsync(MemberQueryObject queryObject, CancellationToken cancellationToken = default)
        => _decorator.GetAllMembersAsync(queryObject, cancellationToken);

    public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        string key = $"memeber-{id}";

       string? cachedMember = await _distributedCache.GetStringAsync(
           key, 
           cancellationToken);

        Member? member;

        if (string.IsNullOrEmpty(cachedMember)) 
        { 
            member = await _decorator.GetByIdAsync(id, cancellationToken);

            if (member is null)
            {
                return member;
            }

            await _distributedCache.SetStringAsync(
                key,
                JsonConvert.SerializeObject(member),
                cancellationToken);

            return member;
        }

        member = JsonConvert.DeserializeObject<Member>(
            cachedMember,
            new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ContractResolver = new PrivateResolver()
            });

        if (member is not null)
        {
            _dbContext.Members.Attach(member);
        }

        return member;
    }

    public Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default) 
        => _decorator.IsEmailUniqueAsync(email, cancellationToken);

    public void Update(Member member) => _decorator.Update(member);
}
