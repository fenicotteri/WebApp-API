﻿
using WebApp.Application.Abstractions.Messaging;
using WebApp.Application.Abstractions.Services;
using WebApp.Domain.Entities;
using WebApp.Domain.Errors;
using WebApp.Domain.Repositories;
using WebApp.Domain.Shared;
using WebApp.Domain.ValueObjects;

namespace WebApp.Application.Members.Commands.UpdateMember;

internal sealed class UpdateMemberCommandHandler : ICommandHandler<UpdateMemberCommand>
{
    private readonly IMemberRepository _memberRepository;
    private readonly ICacheService _cacheService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMemberCommandHandler(
        IMemberRepository memberRepository,
        ICacheService cacheService,
        IUnitOfWork unitOfWork)
    {
        _memberRepository = memberRepository;
        _cacheService = cacheService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        UpdateMemberCommand request,
        CancellationToken cancellationToken)
    {
        Member? member = await _memberRepository.GetByIdAsync(
            request.MemberId,
            cancellationToken);

        if (member is null)
        {
            return Result.Failure(
                DomainErrors.Member.NotFound(request.MemberId));
        }

        Result<FirstName> firstNameResult = FirstName.Create(request.FirstName);
        Result<LastName> lastNameResult = LastName.Create(request.LastName);

        member.ChangeName(
            firstNameResult.Value,
            lastNameResult.Value);

        await _cacheService.RemoveAsync($"member-{request.MemberId}", cancellationToken);
        _memberRepository.Update(member);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
