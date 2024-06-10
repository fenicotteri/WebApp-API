
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Application.Gatherings.Commands.CreateGathering;
using WebApp.Domain.Shared;
using WebApp.Presentation.Contracts.Gatherings;

namespace WebApp.Presentation.Controllers;

[Route("api/gatherings")]
public sealed class GatheringController : Controller
{
    private readonly IMediator _mediator;
    public GatheringController(IMediator mediator)
    {
        _mediator = mediator;
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

        Result<Guid> result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}
