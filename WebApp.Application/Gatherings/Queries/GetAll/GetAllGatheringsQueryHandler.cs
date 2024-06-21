

using AutoMapper;
using WebApp.Application.Abstractions.Messaging;
using WebApp.Application.Members.Queries.GetAll;
using WebApp.Domain.QueryObjects;
using WebApp.Domain.Shared;
using WebApp.Persistence.Repositories;

namespace WebApp.Application.Gatherings.Queries.GetAll;

internal sealed class GetAllGatheringsQueryHandler : IQueryHandler<GetAllGatheringsQuery, AllGatheringsResponse>
{
    private readonly IGatheringRepository _gathering;
    private readonly IMapper _mapper;

    public GetAllGatheringsQueryHandler(IGatheringRepository gathering, IMapper mapper)
    {
        _gathering = gathering;
        _mapper = mapper;
    }

    public async Task<Result<AllGatheringsResponse>> Handle(GetAllGatheringsQuery request, CancellationToken cancellationToken)
    {
        var requestObject = _mapper.Map<GatheringQueryObject>(request.QueryObjectRequest);

        var members = await _gathering.GetAllAsync(requestObject, cancellationToken);

        var response = _mapper.Map<AllGatheringsResponse>(members);

        return response;
    }
}
