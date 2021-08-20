using CurriculumVitae.Application.Cv.Validators;
using CurriculumVitae.Application.WorkExperience.Dtos;
using FluentValidation;

namespace CurriculumVitae.Application.WorkExperience
{
    public class WorkExperienceInputDtoValidator : AbstractValidator<WorkExperienceInputDto>
    {
        public WorkExperienceInputDtoValidator()
        {
            var idNameRefDtoValidator = new IdNameRefDtoValidator();
            
            RuleFor(x => x.CompanyName).NotEmpty();
            RuleFor(x => x.WorkDefinition).NotEmpty();
            RuleFor(x => x.StartDate).NotNull().LessThan(x => x.EndDate);
            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate);
            RuleFor(x => x.Sector).SetValidator(idNameRefDtoValidator);
            RuleFor(x => x.Position).SetValidator(idNameRefDtoValidator);
            RuleFor(x => x.WorkType).SetValidator(idNameRefDtoValidator);
        }
    }
}