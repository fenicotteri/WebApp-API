

using MediatR;
using WebApp.Application.Abstractions.Messaging;
using WebApp.Domain.Shared;

namespace WebApp.Application.Gatherings.Queries.GetAll;

public sealed record GetByCreatorGatheringQuerry(Guid CreatorId) : IQuery<List<GatheringResponse>>;

