
using AutoMapper;
using WebApp.Application.Abstractions;
using WebApp.Domain.Errors;
using WebApp.Domain.Repositories;
using WebApp.Domain.Shared;
using WebApp.Persistence.Repositories;

namespace WebApp.Application.Gatherings.Queries.GetAll;


internal sealed class GetByCreatorGatheringQuerryHandler
    : IQueryHandler<GetByCreatorGatheringQuerry, List<GatheringResponse>>
{
    private readonly IGatheringRepository _gatheringRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IMapper _mapper;

    public GetByCreatorGatheringQuerryHandler(IGatheringRepository gatheringRepository, IMemberRepository memberRepository, IMapper mapper)
    {
        _gatheringRepository = gatheringRepository;
        _memberRepository = memberRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<GatheringResponse>>> Handle(GetByCreatorGatheringQuerry request, CancellationToken cancellationToken)
    {
        var creator = await _memberRepository.GetByIdAsync(request.CreatorId, cancellationToken);

        if (creator is null)
        {
            return Result.Failure<List<GatheringResponse>>(DomainErrors.Member.NotFound(request.CreatorId));
        }

        var gatherings = await _gatheringRepository.GetByCreatorIdAsync(request.CreatorId, cancellationToken);

        var response = _mapper.Map<List<GatheringResponse>>(gatherings);

        return Result.Success(response);
    }
}

