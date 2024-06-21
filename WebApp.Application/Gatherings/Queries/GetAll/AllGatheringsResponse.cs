
namespace WebApp.Application.Gatherings.Queries.GetAll;
public sealed class AllGatheringsResponse
{
    public List<GatheringResponse> Gatherings { get; set; } = null!;
    public int Total { get; set; }
}
