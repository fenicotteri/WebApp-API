

using MediatR;
using WebApp.Application.Abstractions;
using WebApp.Domain.Shared;

namespace WebApp.Application.Gatherings.Queries.GetAll;

public sealed record GetByCreatorGatheringQuerry(Guid CreatorId) : IQuery<List<GatheringResponse>>;

