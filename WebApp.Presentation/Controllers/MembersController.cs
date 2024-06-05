using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Application.Members.Commands.CreateMember;
using WebApp.Application.Members.Queries.GetById;
using WebApp.Presentation.Contracts.Member;

namespace WebApp.Presentation.Controllers;

[Route("api/members")]
public sealed class MembersController : Controller
{
    private readonly IMediator _mediator;
    public MembersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<MemberResponse>> GetMemberById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetMemberByIdQuery(id);

        MemberResponse response = await _mediator.Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> RegisterMember(
    [FromBody] RegisterMemberRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateMemberCommand(
            request.Email,
            request.FirstName,
            request.LastName);

        Guid result = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetMemberById),
            new { id = result },
            result);
    }

}