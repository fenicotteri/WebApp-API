using AutoMapper;
using WebApp.Application.Abstractions.Messaging;
using WebApp.Domain.QueryObjects;
using WebApp.Domain.Repositories;
using WebApp.Domain.Shared;

namespace WebApp.Application.Members.Queries.GetAll;

internal sealed class GetAllMembersQueryHandler :
    IQueryHandler<GetAllMembersQuery, AllMembersResponse>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IMapper _mapper;

    public GetAllMembersQueryHandler(IMemberRepository memberRepository, IMapper mapper)
    {
        _memberRepository = memberRepository;
        _mapper = mapper;
    }

    public async Task<Result<AllMembersResponse>> Handle(GetAllMembersQuery request, CancellationToken cancellationToken)
    {
        var requestObject = _mapper.Map<MemberQueryObject>(request.QueryObjectRequest);

        var members = await _memberRepository.GetAllMembersAsync(requestObject, cancellationToken);

        var response = _mapper.Map<AllMembersResponse>(members);

        return response;
    }
}
