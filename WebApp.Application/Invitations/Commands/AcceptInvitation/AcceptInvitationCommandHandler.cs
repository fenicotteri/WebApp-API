
using MediatR;
using WebApp.Domain.Entities;
using WebApp.Domain.Repositories;
using WebApp.Domain.Shared;
using WebApp.Persistence.Repositories;

namespace WebApp.Application.Invitations.Commands.AcceptInvitation;

internal sealed class AcceptInvitationCommandHandler : IRequestHandler<AcceptInvitationCommand, Unit>
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

    public async Task<Unit> Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
    {
        var gathering = await _gatheringRepository.GetByIdWithInvitationsAsync(request.GatheringId, cancellationToken);

        if (gathering is null)
        {
            return Unit.Value;
        }

        var invitation = gathering.Invitations.FirstOrDefault(x => x.Id == request.GatheringId);

        if (invitation is null)
        {
            return Unit.Value;
        }

        Result<Attendee> attendeeResult = gathering.AcceptInvitation(invitation);

        if (attendeeResult.IsSuccess)
        {
            _attendeeRepository.Add(attendeeResult.Value);   
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;

    }
}

