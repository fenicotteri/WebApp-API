using MediatR;
using WebApp.Domain.Errors;
using WebApp.Domain.Repositories;
using WebApp.Domain.Shared;

namespace WebApp.Application.Members.Queries.GetById;

internal sealed class GetMemberByIdQueryHandler
    : IRequestHandler<GetMemberByIdQuery, Result<MemberResponse>>
{
    private readonly IMemberRepository _memberRepository;

    public GetMemberByIdQueryHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<Result<MemberResponse>> Handle(
        GetMemberByIdQuery request,
        CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(
            request.MemberId,
            cancellationToken);

        if (member is null)
        {
            return Result.Failure<MemberResponse>(DomainErrors.Member.NotFound(request.MemberId));
        }

        // mapper goes here
        var response = new MemberResponse(
            member.Email.Value,
            member.FirstName.Value, 
            member.LastName.Value, 
            member.CreatedOnUtc);

        return response;
    }
}
