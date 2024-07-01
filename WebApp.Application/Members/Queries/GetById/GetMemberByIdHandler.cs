using AutoMapper;
using MediatR;
using WebApp.Application.Abstractions.Messaging;
using WebApp.Application.Abstractions.Services;
using WebApp.Domain.Errors;
using WebApp.Domain.Repositories;
using WebApp.Domain.Shared;

namespace WebApp.Application.Members.Queries.GetById;

internal sealed class GetMemberByIdQueryHandler
    : IQueryHandler<GetMemberByIdQuery, MemberResponse>
{
    private readonly IMemberRepository _memberRepository;
    private readonly ICacheService _cacheService;
    private readonly IMapper _mapper;

    public GetMemberByIdQueryHandler(IMemberRepository memberRepository, ICacheService cacheService, IMapper mapper)
    {
        _memberRepository = memberRepository;
        _cacheService = cacheService;
        _mapper = mapper;
    }

    public async Task<Result<MemberResponse>> Handle(
        GetMemberByIdQuery request,
        CancellationToken cancellationToken)
    {
        var member = await _cacheService.GetAsync(
            $"member-{request.MemberId}",
            async () =>
            {
                var member = await _memberRepository.GetByIdAsync(
                    request.MemberId,
                    cancellationToken);

                return member;

            }, cancellationToken);
    

        if (member is null)
        {
            return Result.Failure<MemberResponse>(DomainErrors.Member.NotFound(request.MemberId));
        }

        // mapper goes here
        var response = _mapper.Map<MemberResponse>(member);
          
        return response;
    }
}
