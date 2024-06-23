using Microsoft.EntityFrameworkCore;
using WebApp.Domain.Entities;
using WebApp.Domain.QueryObjects;
using WebApp.Domain.Repositories;
using WebApp.Domain.ValueObjects;

namespace WebApp.Persistence.Repositories;

public sealed class MemberRepository : IMemberRepository
{
    private readonly DataContext _dbContext;

    public MemberRepository(DataContext dbContext) => _dbContext = dbContext;
    public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _dbContext
            .Members
            .FirstOrDefaultAsync(member => member.Id == id, cancellationToken);

    public async Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default) =>
       !await _dbContext
           .Members
           .AnyAsync(member => member.Email == email, cancellationToken);

    public void Add(Member member) =>
        _dbContext.Members.Add(member);

    public void Update(Member member) =>
        _dbContext.Members.Update(member);

    public async Task<List<Member>> GetAllMembersAsync(MemberQueryObject queryObject, CancellationToken cancellationToken = default)
    {
        IQueryable<Member> membersQuery = _dbContext.Members;

        if (!string.IsNullOrWhiteSpace(queryObject.SearchTerm))
        {
            membersQuery = membersQuery.Where(m => ((string)m.Email).Contains(queryObject.SearchTerm));
        }

        var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;
        membersQuery = membersQuery.Skip(skipNumber).Take(queryObject.PageSize);

        return await membersQuery.ToListAsync(cancellationToken: cancellationToken);

    }
}

