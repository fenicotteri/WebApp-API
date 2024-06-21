
namespace WebApp.Domain.QueryObjects;

public class GatheringQueryObject
{
    public string? SortOrder { get; set; }
    public string? SortColumn { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
