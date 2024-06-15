
using MediatR;
using System.Net.Mail;
using WebApp.Application.Abstractions.Services;
using WebApp.Domain.DomainEvents;
using WebApp.Domain.Repositories;
using WebApp.Infrastructure.Services;
using WebApp.Persistence.Repositories;

namespace WebApp.Application.Invitations.Events;

internal sealed class InvitationAcceptedDomainEventHandler
    : INotificationHandler<InvitationAcceptedDomainEvent>
{
    private readonly IGatheringRepository _gatheringRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IEmailService _emailService;

    public InvitationAcceptedDomainEventHandler(IGatheringRepository gatheringRepository, IMemberRepository memberRepository, IEmailService emailService)
    {
        _gatheringRepository = gatheringRepository;
        _memberRepository = memberRepository;
        _emailService = emailService;
    }

    public async Task Handle(InvitationAcceptedDomainEvent notification, CancellationToken cancellationToken)
    {
        var gathering = await _gatheringRepository.GetByIdWithCreatorAsync(notification.Attendee.GatheringId, cancellationToken);
        var member = await _memberRepository.GetByIdAsync(notification.Attendee.MemberId, cancellationToken);

        if (gathering == null || member == null)
        {
            return;
        }

        string FilePath = "C:/Users/mag20/source/repos/WebApp/WebApp.Infrastructure/Templates/AcceptedInvitation.html";
        string EmailTemplateText = File.ReadAllText(FilePath);
        
        EmailTemplateText = string.Format(EmailTemplateText, 
            gathering.Creator.FirstName.Value,
            DateTime.Now.Date.ToShortDateString(),
            member.FirstName.Value,
            gathering.Name,
            gathering.ScheduledAtUtc);

        EmailData emailData = new(
            gathering.Creator.Email.Value,
            gathering.Creator.FirstName.Value,
            "Invitation accepted.", 
            EmailTemplateText);

        await _emailService.SendInvitationAcceptedEmailAsync(emailData, cancellationToken);
    }
}
