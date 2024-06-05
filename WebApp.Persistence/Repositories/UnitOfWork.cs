
using Domain.Abstractions;
using WebApp.Persistence;

namespace WebApp.Infastructure.Repositories;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dbContext;

    public UnitOfWork(DataContext dbContext) => _dbContext = dbContext;
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

}
