
using WebApp.Application.Members.Queries.GetById;

namespace WebApp.Application.Members.Queries.GetAll;

public sealed class AllMembersResponse
{
    public List<MemberResponse> Members { get; set; } = null!;
    public int Total {  get; set; } 
}
