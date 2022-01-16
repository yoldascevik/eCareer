using FluentValidation;

namespace Company.Application.Company.Commands.UpdateCompanyEmail;

public class UpdateCompanyEmailCommandValidator : AbstractValidator<UpdateCompanyEmailCommand>
{
    public UpdateCompanyEmailCommandValidator()
    {
        RuleFor(x => x.CompanyId).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}