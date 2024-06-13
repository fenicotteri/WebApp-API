
namespace WebApp.Application.Gatherings.Queries.GetAll;


public sealed class GatheringResponse
{
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime ScheduledAtUtc { get; set; }
    public DateTime InvitationsExpireAtUtc { get; set; }
}
   