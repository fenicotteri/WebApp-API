using Microsoft.EntityFrameworkCore;
using WebApp.Domain.Abstractions;
using WebApp.Domain.Entities;
using WebApp.Persistence;


namespace WebApp.Infastructure.Repositories;

public sealed class MemberRepository : IMemberRepository
{
    private readonly DataContext _dbContext;

    public MemberRepository(DataContext dbContext) => _dbContext = dbContext;
    public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _dbContext
            .Members
            .FirstOrDefaultAsync(member => member.Id == id, cancellationToken);

    public async Task<bool> IsEmailUniqueAsync( string email, CancellationToken cancellationToken = default) =>
       !await _dbContext
           .Members
           .AnyAsync(member => member.Email == email, cancellationToken);

    public void Add(Member member) =>
        _dbContext.Members.Add(member);

    public void Update(Member member) =>
        _dbContext.Members.Update(member);
}

