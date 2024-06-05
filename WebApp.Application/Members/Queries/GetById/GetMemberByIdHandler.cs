using MediatR;
using WebApp.Domain.Abstractions;

namespace WebApp.Application.Members.Queries.GetById;

internal sealed class GetMemberByIdQueryHandler
    : IRequestHandler<GetMemberByIdQuery, MemberResponse>
{
    private readonly IMemberRepository _memberRepository;

    public GetMemberByIdQueryHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<MemberResponse> Handle(
        GetMemberByIdQuery request,
        CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(
            request.MemberId,
            cancellationToken);

        if (member is null)
        {
            throw new ArgumentNullException(nameof(member));
        }

        // mapper goes here
        var response = new MemberResponse(member.Email, member.FirstName, member.LastName, member.CreatedAt);

        return response;
    }
}
