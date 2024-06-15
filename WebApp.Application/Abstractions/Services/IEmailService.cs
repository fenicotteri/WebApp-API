using WebApp.Domain.Entities;
using WebApp.Infrastructure.Services;

namespace WebApp.Application.Abstractions.Services;

public interface IEmailService
{
    Task SendInvitationAcceptedEmailAsync(EmailData emailData, CancellationToken cancellationToken = default);
}
