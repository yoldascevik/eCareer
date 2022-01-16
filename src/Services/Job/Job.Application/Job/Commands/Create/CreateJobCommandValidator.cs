using FluentValidation;
using Job.Application.Job.Validators;

namespace Job.Application.Job.Commands.Create;

public class CreateJobCommandValidator : AbstractValidator<CreateJobCommand>
{
    public CreateJobCommandValidator()
    {
        var idNameRefDtoValidator = new IdNameRefDtoValidator();
            
        RuleFor(x => x.Company).NotNull();
        RuleFor(x => x.Company.RefId).NotNull().NotEmpty();
        RuleFor(x => x.Company.Name).NotNull().NotEmpty();
        RuleFor(x => x.Job.Language).SetValidator(idNameRefDtoValidator);
        RuleFor(x => x.Job.Sector).SetValidator(idNameRefDtoValidator);
        RuleFor(x => x.Job.JobPosition).SetValidator(idNameRefDtoValidator);
        RuleFor(x => x.Job.Title).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(x => x.Job.Description).NotNull().NotEmpty();
        RuleFor(x => x.Job.PersonCount).GreaterThan((short) 0);
        RuleFor(x => x.Job.IsCanDisabilities).NotNull();
        RuleFor(x => x.Job.Gender).NotNull();
            
        RuleFor(x => x.Job.MinExperienceYear)
            .LessThanOrEqualTo(x=> x.Job.MaxExperienceYear)
            .When(x=> x.Job.MaxExperienceYear.HasValue);
            
        RuleFor(x => x.Job.MaxExperienceYear)
            .GreaterThanOrEqualTo(x=> x.Job.MinExperienceYear)
            .When(x=> x.Job.MinExperienceYear.HasValue);
    }
}