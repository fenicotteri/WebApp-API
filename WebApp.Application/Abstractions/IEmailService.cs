
using WebApp.Domain.Entities;

namespace WebApp.Application.Abstractions;

public interface IEmailService
{
    Task SendInvitationAcceptedEmailAsync(Gathering gathering, CancellationToken cancellationToken = default);
}
