using Microsoft.EntityFrameworkCore;
using WebApp.Domain.Primitieves;
using WebApp.Domain.Repositories;
using WebApp.Persistence;

namespace WebApp.Infastructure.Repositories;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dbContext;

    public UnitOfWork(DataContext dbContext) => _dbContext = dbContext;
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var modifiedEntries = _dbContext.ChangeTracker.Entries()
            .Where(e => e.Entity is AuditableEntity && 
            (e.State == EntityState.Modified));

        foreach(var entry in modifiedEntries)
        {
            ((AuditableEntity)entry.Entity).UpdateTime();
        }

        return _dbContext.SaveChangesAsync(cancellationToken);
    }

}
