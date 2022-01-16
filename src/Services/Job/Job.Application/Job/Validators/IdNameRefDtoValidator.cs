using FluentValidation;
using Job.Application.Job.Dtos;

namespace Job.Application.Job.Validators;

public class IdNameRefDtoValidator: AbstractValidator<IdNameRefDto>
{
    public IdNameRefDtoValidator()
    {
        RuleFor(x => x).NotNull();
        RuleFor(x => x.RefId).NotNull().NotEmpty();
        RuleFor(x => x.Name).NotNull().NotEmpty();
    }
}