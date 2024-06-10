
using MediatR;
using WebApp.Domain.Entities;
using WebApp.Domain.Errors;
using WebApp.Domain.Repositories;
using WebApp.Domain.Shared;
using WebApp.Domain.ValueObjects;


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
        Result<Email> emailResult = Email.Create(request.Email);
        
        if (!await _memberRepository.IsEmailUniqueAsync(emailResult.Value, cancellationToken))
        {
            return Result.Failure<Guid>(DomainErrors.Member.EmailAlreadyInUse);
        }

        // how to catch if Result.isFailure 
        Result<FirstName> firstNameResult = FirstName.Create(request.FirstName);
        Result<LastName> lastNameResult = LastName.Create(request.LastName);

        var member = new Member(
            new Guid(), 
            emailResult.Value, 
            firstNameResult.Value, 
            lastNameResult.Value);

        _memberRepository.Add(member);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return member.Id;
    }
}
