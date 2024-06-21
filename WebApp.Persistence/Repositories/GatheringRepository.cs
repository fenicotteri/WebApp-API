
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading;
using WebApp.Domain.Entities;
using WebApp.Domain.QueryObjects;

namespace WebApp.Persistence.Repositories;
public class GatheringRepository : IGatheringRepository
{
    private readonly DataContext _dbContext;

    public GatheringRepository(DataContext dbContext) => _dbContext = dbContext;

    public void Add(Gathering gathering)
    {
        _dbContext.Gatherings.Add(gathering);
    }

    public async Task<List<Gathering>> GetAllAsync(GatheringQueryObject queryObject, CancellationToken cancellationToken = default)
    {
        IQueryable<Gathering> membersQuery = _dbContext.Gatherings;

        Expression<Func<Gathering, object>> keySelector = queryObject.SortColumn switch
        {
            "time" => gathering => gathering.ScheduledAtUtc,
            "time_acc" => gathering => gathering.InvitationsExpireAtUtc,
            _ => gathering => gathering.Id
        };

        if (queryObject.SortOrder?.ToLower() == "desc")
        {
            membersQuery = membersQuery.OrderByDescending(keySelector);
        }
        else
        {
            membersQuery.OrderBy(keySelector);
        }

        return await membersQuery
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Gathering>> GetByCreatorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Gatherings
            .AsNoTracking()
            .Include(gathering => gathering.Creator)
            .Where(c => c.Creator.Id == id)
            .ToListAsync(cancellationToken);
    }

    public async Task<Gathering?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Gatherings
            .FirstOrDefaultAsync(gathering => gathering.Id == id , cancellationToken);
    }

    public async Task<Gathering?> GetByIdWithCreatorAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Gatherings
            .Include(gathering => gathering.Creator)
            .FirstOrDefaultAsync(gathering => gathering.Id == id, cancellationToken);
    }

    public async Task<Gathering?> GetByIdWithInvitationsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Gatherings
            .Include(gathering => gathering.Invitations)
            .FirstOrDefaultAsync(gathering => gathering.Id == id, cancellationToken);
    }

    public async Task<List<Gathering>> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Gatherings
            .OrderBy(gathering => gathering.Name)
            .Where(gathering => gathering.Name == name)
            .ToListAsync();
    }

    public void Remove(Gathering gathering)
    {
        _dbContext.Gatherings.Remove(gathering);
    }
}
