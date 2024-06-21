namespace WebApp.Domain;

public sealed class MemberQueryObjectRequest
{
    /// <summary>Text to search be email</summary>
    public string? SearchTerm { get; set; }
    /// <summary>Pagination.</summary>
    public int PageNumber { get; set; } = 1;
    /// <summary>Pagination.</summary>
    public int PageSize { get; set; } = 20;
}
