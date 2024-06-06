using Domain.Abstractions;
using MediatR;
using WebApp.Domain.Abstractions;
using WebApp.Domain.Entities;
using WebApp.Domain.Errors;
using WebApp.Domain.Shared;


namespace WebApp.Application.Members.Commands.CreateMember;

internal sealed class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, Result<Guid>>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMemberCommandHandler(
        IMemberRepository memberRepository,
        IUnitOfWork unitOfWork)
    {
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid>> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        if (!await _memberRepository.IsEmailUniqueAsync(request.Email, cancellationToken))
        {
            return Result.Failure<Guid>(DomainErrors.Member.EmailAlreadyInUse);
        }

        var member = new Member(
            new Guid(), 
            request.Email, 
            request.FirstName, 
            request.LastName);

        _memberRepository.Add(member);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return member.Id;
    }
}
