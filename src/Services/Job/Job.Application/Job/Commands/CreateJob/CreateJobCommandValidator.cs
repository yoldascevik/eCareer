using FluentValidation;

namespace Job.Application.Job.Commands.CreateJob
{
    public class CreateJobCommandValidator : AbstractValidator<CreateJobCommand>
    {
        public CreateJobCommandValidator()
        {
            RuleFor(x => x.CompanyId).NotNull();
            RuleFor(x => x.Job.LanguageId).NotNull().NotEmpty();
            RuleFor(x => x.Job.SectorId).NotNull().NotEmpty();
            RuleFor(x => x.Job.JobPositionId).NotNull().NotEmpty();
            RuleFor(x => x.Job.Title).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.Job.Description).NotNull().NotEmpty();
            RuleFor(x => x.Job.PersonCount).GreaterThan((short) 0);
            RuleFor(x => x.Job.IsCanDisabilities).NotNull();
            RuleFor(x => x.Job.Gender).NotNull();
            RuleFor(x => x.Job.MinExperienceYear).LessThanOrEqualTo(x=> x.Job.MaxExperienceYear);
            RuleFor(x => x.Job.MaxExperienceYear).GreaterThanOrEqualTo(x=> x.Job.MinExperienceYear);
        }
    }
}