
using FluentValidation;
using WebApp.Domain.ValueObjects;

namespace WebApp.Application.Members.Commands.CreateMember;

internal sealed class CreateCustomerCommandValidator : AbstractValidator<CreateMemberCommand>
{
    public CreateCustomerCommandValidator() 
    {
        RuleFor(x => x.Email).NotEmpty();

        RuleFor(x => x.FirstName).NotEmpty()
            .MaximumLength(FirstName.MaxLength)
            .WithMessage($"First name shouldnt be grater than {FirstName.MaxLength}");

        RuleFor(x => x.LastName).NotEmpty()
            .MaximumLength(LastName.MaxLength)
            .WithMessage($"Last name shouldnt be grater than {LastName.MaxLength}");
    }
}
