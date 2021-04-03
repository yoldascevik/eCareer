using Career.Shared.Timing;
using FluentValidation;
using Job.Application.Job.Commands.Publish;

namespace Job.Application.Job.Commands.PublishJob
{
    public class PublishJobCommandValidator: AbstractValidator<PublishJobCommand>
    {
        public PublishJobCommandValidator()
        {
            RuleFor(x => x.JobId).NotEmpty();
            RuleFor(x => x.ValidityDate)
                .NotEmpty()
                .GreaterThan(Clock.Now);
        }
    }
}