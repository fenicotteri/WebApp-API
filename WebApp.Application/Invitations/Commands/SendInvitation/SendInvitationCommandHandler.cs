
using MediatR;
using WebApp.Domain.Entities;
using WebApp.Domain.Repositories;
using WebApp.Domain.Shared;
using WebApp.Persistence.Repositories;

namespace WebApp.Application.Invitations.Commands.SendInvitation;

internal sealed class SendInvitationCommandHandler : IRequestHandler<SendInvitationCommand, Unit>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IGatheringRepository _gatheringRepository;
    private readonly IInvitationRepository _invitationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SendInvitationCommandHandler(
        IMemberRepository memberRepository,
        IGatheringRepository gatheringRepository, 
        IInvitationRepository invitationRepository,
        IUnitOfWork unitOfWork)
    {
        _memberRepository = memberRepository;
        _invitationRepository = invitationRepository;
        _gatheringRepository = gatheringRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(SendInvitationCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(request.MemberId, cancellationToken);
        var gathering = await _gatheringRepository.GetByIdAsync(request.GatheringId, cancellationToken);

        if (member is null || gathering is null)
        {
            return Unit.Value;
        }

        Result<Invitation> invitationResult = gathering.SendInvitation(member);

        if (invitationResult.IsFailure)
        {
            return Unit.Value;
        }

        _invitationRepository.Add(invitationResult.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
