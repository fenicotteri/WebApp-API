namespace WebApp.Domain.QueryObjects;

public class MemberQueryObject
{
    public string? SearchTerm { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
