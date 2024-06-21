
using WebApp.Domain.Entities;
using WebApp.Domain.QueryObjects;

namespace WebApp.Persistence.Repositories;

public interface IGatheringRepository
{
    Task<List<Gathering>> GetByNameAsync(string name, CancellationToken cancellationToken = default);

    Task<Gathering?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Gathering?> GetByIdWithCreatorAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<Gathering>> GetByCreatorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Gathering>> GetAllAsync(GatheringQueryObject queryObject, CancellationToken cancellationToken = default);

    Task<Gathering?> GetByIdWithInvitationsAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(Gathering gathering);

    void Remove(Gathering gathering);
}

