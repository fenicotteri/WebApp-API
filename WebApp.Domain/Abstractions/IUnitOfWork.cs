using System.Threading;
using System.Threading.Tasks;

namespace Domain.Abstractions;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}