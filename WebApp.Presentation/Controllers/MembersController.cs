﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Application.Invitations.Commands.AcceptInvitation;
using WebApp.Application.Members.Commands.CreateMember;
using WebApp.Application.Members.Commands.UpdateMember;
using WebApp.Application.Members.Queries.GetAll;
using WebApp.Application.Members.Queries.GetById;
using WebApp.Domain;
using WebApp.Domain.Shared;
using WebApp.Presentation.Contracts.Members;

namespace WebApp.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class MembersController : ApiController
{
    public MembersController(IMediator mediator) 
        : base(mediator)
    {
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MemberResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMemberById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetMemberByIdQuery(id);

        Result<MemberResponse> response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AllMembersResponse))]
    public async Task<IActionResult> GetMembers(
        [FromQuery] MemberQueryObjectRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetAllMembersQuery(request);

        Result<AllMembersResponse> response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterMember(
    [FromBody] RegisterMemberRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateMemberCommand(
            request.Email,
            request.FirstName,
            request.LastName);

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return CreatedAtAction(
            nameof(GetMemberById),
            new { id = result.Value },
            result.Value
        );
    }

    [HttpPost("accept")]
    public async Task<IActionResult> AcceptInvitation(
        Guid gatheringId, 
        Guid invitationId,
        CancellationToken cancellationToken)
    {
        var command = new AcceptInvitationCommand(
            gatheringId,
            invitationId);

        Result result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);  
        }

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateMember(
        Guid id,
        [FromBody] UpdateMemberRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateMemberCommand(
            id,
            request.FirstName,
            request.LastName);

        Result result = await _sender.Send(
            command,
            cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return NoContent();
    }

}