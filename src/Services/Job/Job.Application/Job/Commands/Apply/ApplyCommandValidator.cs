using FluentValidation;

namespace Job.Application.Job.Commands.Apply
{
    public class AplyCommandValidator: AbstractValidator<ApplyCommand>
    {
        public AplyCommandValidator()
        {
            RuleFor(x => x.JobId).NotEmpty();
            RuleFor(x => x.CandidateDto.CvId).NotNull().NotEmpty();
            RuleFor(x => x.CandidateDto.UserId).NotNull().NotEmpty();
        }
    }
}