namespace WebApp.Application.Members.Queries.GetById;

public sealed class MemberResponse
{
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
}
    
