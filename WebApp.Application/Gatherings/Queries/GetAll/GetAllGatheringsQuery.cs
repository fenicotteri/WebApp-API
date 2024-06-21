
using WebApp.Application.Abstractions.Messaging;

namespace WebApp.Application.Gatherings.Queries.GetAll;

public sealed record GetAllGatheringsQuery(GatheringQueryObjectRequest QueryObjectRequest) : IQuery<AllGatheringsResponse>;
