
using Domain.Primitives;

namespace WebApp.Domain.Primitives;

public abstract class AggregateRoot : Entity
{
    protected AggregateRoot(Guid id)
        : base(id)
    { 
    }
    protected AggregateRoot()
    {
    }
}
