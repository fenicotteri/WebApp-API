
using Microsoft.EntityFrameworkCore;
using System.Threading;
using WebApp.Domain.Entities;

namespace WebApp.Persistence.Repositories;
public class GatheringRepository : IGatheringRepository
{
    private readonly DataContext _dbContext;

    public GatheringRepository(DataContext dbContext) => _dbContext = dbContext;

    public void Add(Gathering gathering)
    {
        _dbContext.Gatherings.Add(gathering);
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
