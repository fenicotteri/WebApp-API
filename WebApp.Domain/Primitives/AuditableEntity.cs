using Domain.Primitives;

namespace WebApp.Domain.Primitieves;

public abstract class AuditableEntity : Entity
{
    public AuditableEntity(Guid id) : base(id)
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public DateTime CreatedAt { get; private init; }
    public DateTime UpdatedAt { get; private set; }
    public void UpdateTime()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}

