
using FluentValidation;
using WebApp.Domain.ValueObjects;

namespace WebApp.Application.Members.Commands.CreateMember;

internal sealed class CreateCustomerCommandValidator : AbstractValidator<CreateMemberCommand>
{
    public CreateCustomerCommandValidator() 
    {

        RuleFor(x => x.Email).EmailAddress().NotEmpty();

        RuleFor(x => x.FirstName).NotEmpty()
            .MaximumLength(FirstName.MaxLength);

        RuleFor(x => x.LastName).NotEmpty()
            .MaximumLength(LastName.MaxLength);
    }
}
