
using MediatR;
using WebApp.Application.Abstractions;
using WebApp.Domain.DomainEvents;
using WebApp.Persistence.Repositories;

namespace WebApp.Application.Invitations.Events;

internal sealed class InvitationAcceptedDomainEventHandler
    : INotificationHandler<InvitationAcceptedDomainEvent>
{
    private readonly IGatheringRepository _gatheringRepository;
    private readonly IEmailService _emailService;

    public InvitationAcceptedDomainEventHandler(IGatheringRepository gatheringRepository, IEmailService emailService)
    {
        _gatheringRepository = gatheringRepository;
        _emailService = emailService;
    }

    public async Task Handle(InvitationAcceptedDomainEvent notification, CancellationToken cancellationToken)
    {
        var gathering = await _gatheringRepository.GetByIdAsync(notification.GatheringId);

        if (gathering == null)
        {
            return;
        }

        await _emailService.SendInvitationAcceptedEmailAsync(gathering, cancellationToken);
    }
}
