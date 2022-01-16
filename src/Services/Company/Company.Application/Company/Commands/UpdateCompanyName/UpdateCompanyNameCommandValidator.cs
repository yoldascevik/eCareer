using FluentValidation;

namespace Company.Application.Company.Commands.UpdateCompanyName;

public class UpdateCompanyNameCommandValidator : AbstractValidator<UpdateCompanyNameCommand>
{
    public UpdateCompanyNameCommandValidator()
    {
        RuleFor(x => x.CompanyId).NotEmpty();
        RuleFor(x => x.CompanyName) 
            .MaximumLength(100) 
            .MinimumLength(3) 
            .NotEmpty();
    }
}