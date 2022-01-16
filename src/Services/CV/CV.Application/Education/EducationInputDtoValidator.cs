using CurriculumVitae.Application.Cv.Validators;
using CurriculumVitae.Application.Education.Dtos;
using FluentValidation;

namespace CurriculumVitae.Application.Education;

public class EducationInputDtoValidator : AbstractValidator<EducationInputDto>
{
    public EducationInputDtoValidator()
    {
        var idNameRefDtoValidator = new IdNameRefDtoValidator();
            
        RuleFor(x => x.SchoolName).NotEmpty();
        RuleFor(x => x.Section).NotEmpty();
        RuleFor(x => x.StartDate).NotNull().LessThan(x => x.EndDate);
        RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate);
        RuleFor(x => x.EducationLevel).SetValidator(idNameRefDtoValidator);
        RuleFor(x => x.EducationType).SetValidator(idNameRefDtoValidator);
        RuleFor(x => x.ScholarshipType).SetValidator(idNameRefDtoValidator);
    }
}