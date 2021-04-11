using Career.Shared.Timing;
using FluentValidation;

namespace Job.Application.Job.Commands.Publish
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