
using MediatR;
using WebApp.Domain.Entities;
using WebApp.Domain.Errors;
using WebApp.Domain.Repositories;
using WebApp.Domain.Shared;
using WebApp.Persistence.Repositories;

namespace WebApp.Application.Invitations.Commands.AcceptInvitation;

internal sealed class AcceptInvitationCommandHandler : IRequestHandler<AcceptInvitationCommand, Result>
{
    private readonly IGatheringRepository _gatheringRepository;
    private readonly IAttendeeRepository _attendeeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AcceptInvitationCommandHandler(IGatheringRepository gatheringRepository, IAttendeeRepository attendeeRepository, IUnitOfWork unitOfWork)
    {
        _gatheringRepository = gatheringRepository;
        _attendeeRepository = attendeeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
    {
        var gathering = await _gatheringRepository.GetByIdWithInvitationsAsync(request.GatheringId, cancellationToken);

        if (gathering is null)
        {
            return Result.Failure(DomainErrors.Gathering.NotFound(request.GatheringId));
        }

        var invitation = gathering.Invitations.FirstOrDefault(x => x.Id == request.InvitationId);

        if (invitation is null)
        {
            return Result.Failure(DomainErrors.Invitation.NotFound(request.InvitationId));
        }

        Result<Attendee> attendeeResult = gathering.AcceptInvitation(invitation);

        if (attendeeResult.IsFailure)
        {
            return Result.Failure(attendeeResult.Error);
        }

        _attendeeRepository.Add(attendeeResult.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();

    }
}

