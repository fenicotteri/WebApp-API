
using MediatR;
using WebApp.Domain.Entities;
using WebApp.Domain.Errors;
using WebApp.Domain.Repositories;
using WebApp.Domain.Shared;
using WebApp.Persistence.Repositories;

namespace WebApp.Application.Gatherings.Commands.CreateGathering;

public sealed class CreateGatheringCommandHandler : IRequestHandler<CreateGatheringCommand, Result<Guid>>
{
    private readonly IGatheringRepository _gatheringRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateGatheringCommandHandler(IGatheringRepository gatheringRepository, IMemberRepository memberRepository, IUnitOfWork unitOfWork)
    {
        _gatheringRepository = gatheringRepository;
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateGatheringCommand request, CancellationToken cancellationToken)
    {
        var creator = await _memberRepository.GetByIdAsync(request.CreatorId, cancellationToken);

        if (creator is null)
        {
            return Result.Failure<Guid>(DomainErrors.Member.NotFound(request.CreatorId));
        }

        Gathering gathering = Gathering.Create(
            new Guid(),
            creator,
            request.scheduledAtUtc,
            request.Name,
            request.Location,
            request.MaximumNumberOfAttendees,
            request.InvitationsValidBeforeInHours);

        _gatheringRepository.Add(gathering);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return gathering.Id;
    }
}
