using Domain.Primitives;

namespace WebApp.Domain.Primitieves;

public abstract class AuditableEntity : Entity
{
    protected AuditableEntity(Guid id) : base(id)
    {
        CreatedAt = DateTime.UtcNow;
    }

    public DateTime CreatedAt { get; private init; }
    public DateTime ModifedAt { get; private set; }
    public void UpdateTime()
    {
        ModifedAt = DateTime.UtcNow;
    }
}

