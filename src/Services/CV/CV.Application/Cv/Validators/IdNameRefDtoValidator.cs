using CurriculumVitae.Application.Cv.Dtos;
using FluentValidation;

namespace CurriculumVitae.Application.Cv.Validators;

public class IdNameRefDtoValidator : AbstractValidator<IdNameRefDto>
{
    public IdNameRefDtoValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Name).NotNull();
    }
}