using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApp.Domain.Primitieves;
using WebApp.Domain.Primitives;
using WebApp.Domain.Repositories;

namespace WebApp.Persistence.Repositories;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dbContext;
    private readonly IPublisher _publisher;

    public UnitOfWork(DataContext dbContext, IPublisher publisher) 
    { 
        _publisher = publisher;
        _dbContext = dbContext; 
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities();
        await PublishingDomainEvents();

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditableEntities()
    {
        IEnumerable<EntityEntry<AuditableEntity>> entries =
           _dbContext
               .ChangeTracker
               .Entries<AuditableEntity>();

        foreach (EntityEntry<AuditableEntity> entityEntry in entries)
        {

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Entity.UpdateTime();
            }
        }
    }

    private async Task PublishingDomainEvents()
    {
        var domainEvents = _dbContext.ChangeTracker.Entries<AggregateRoot>()
            .Select(x => x.Entity)
            .SelectMany(aggregateRoot =>
            {
                var domainEvents = aggregateRoot.GetDomainEvents();

                return domainEvents;
            });

        foreach (var domainEvent in domainEvents)
        {
             await _publisher.Publish(domainEvent);
        }
    }

}
