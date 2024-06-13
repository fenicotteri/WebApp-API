
using WebApp.Domain.Entities;
using WebApp.Infrastructure.Services;

namespace WebApp.Application.Abstractions;

public interface IEmailService
{
    Task SendInvitationAcceptedEmailAsync(Gathering gathering, EmailData emailData, CancellationToken cancellationToken = default);
}
