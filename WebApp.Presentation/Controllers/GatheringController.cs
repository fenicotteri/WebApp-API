
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Application.Gatherings.Commands.CreateGathering;
using WebApp.Application.Gatherings.Queries.GetAll;
using WebApp.Application.Invitations.Commands.SendInvitation;
using WebApp.Application.Members.Queries.GetAll;
using WebApp.Domain;
using WebApp.Domain.Shared;
using WebApp.Presentation.Contracts.Gatherings;

namespace WebApp.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class GatheringController : ApiController
{
    public GatheringController(IMediator mediator) 
        : base(mediator)
    {
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GatheringResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<GatheringResponse>>> GetGatheringByCreator(
        Guid creatorId,
        CancellationToken cancellationToken)
    {
        var query = new GetByCreatorGatheringQuerry(creatorId);

        Result<List<GatheringResponse>> response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AllGatheringsResponse))]
    public async Task<IActionResult> GetGatherings(
        [FromQuery] GatheringQueryObjectRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetAllGatheringsQuery(request);

        Result<AllGatheringsResponse> response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> RegisterGathering(
    [FromBody] RegisterGatheringRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateGatheringCommand(
            request.CreatorId,
            request.scheduledAtUtc,
            request.Name,
            request.Location,
            request.MaximumNumberOfAttendees,
            request.InvitationsValidBeforeInHours);

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Created(string.Empty, new { id = result.Value });
    }

    [HttpPost("invite")]
    public async Task<ActionResult> SendInvitation(
        Guid gatheringId, 
        Guid memberId,
        CancellationToken cancellationToken)
    {
        var command = new SendInvitationCommand(
            memberId,
            gatheringId);

        Result result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Created(string.Empty, result);
    }

}
