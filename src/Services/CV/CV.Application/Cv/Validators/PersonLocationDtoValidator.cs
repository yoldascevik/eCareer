using CurriculumVitae.Application.Cv.Dtos;
using FluentValidation;

namespace CurriculumVitae.Application.Cv.Validators;

public class PersonLocationDtoValidator : AbstractValidator<PersonLocationDto>
{
    public PersonLocationDtoValidator()
    {
        RuleFor(x => x.Country).SetValidator(new IdNameRefDtoValidator());
        RuleFor(x => x.City).SetValidator(new IdNameRefDtoValidator());
    }
}