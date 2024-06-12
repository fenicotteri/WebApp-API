
using WebApp.Domain.Entities;
using WebApp.Application.Abstractions;

namespace WebApp.Infrastructure.Services;
internal sealed class EmailService : IEmailService
{
    public Task SendInvitationAcceptedEmailAsync(Gathering gathering, CancellationToken cancellationToken = default) =>
        Task.CompletedTask;
}
