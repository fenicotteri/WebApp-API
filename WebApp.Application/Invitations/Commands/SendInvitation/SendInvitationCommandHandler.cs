
using MediatR;
using WebApp.Domain.Entities;
using WebApp.Domain.Errors;
using WebApp.Domain.Repositories;
using WebApp.Domain.Shared;
using WebApp.Persistence.Repositories;

namespace WebApp.Application.Invitations.Commands.SendInvitation;

internal sealed class SendInvitationCommandHandler : IRequestHandler<SendInvitationCommand, Result>
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

    public async Task<Result> Handle(SendInvitationCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(request.MemberId, cancellationToken);
       
        if (member is null)
        {
            return Result.Failure(DomainErrors.Member.NotFound(request.MemberId));
        }

        var gathering = await _gatheringRepository.GetByIdWithCreatorAsync(request.GatheringId, cancellationToken);

        if (gathering is null)
        {
            return Result.Failure(DomainErrors.Gathering.NotFound(request.GatheringId));
        }

        Result<Invitation> invitationResult = gathering.SendInvitation(member);

        if (invitationResult.IsFailure)
        {
            return Result.Failure(invitationResult.Error);
        }

        _invitationRepository.Add(invitationResult.Value);

        await _unitOfWork
            .SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
