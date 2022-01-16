using FluentValidation;

namespace Job.Application.Job.Commands.UpdateJobTags;

public class UpdateJobTagsCommandValidator: AbstractValidator<UpdateJobTagsCommand>
{
    public UpdateJobTagsCommandValidator()
    {
        RuleFor(x => x.JobId).NotNull().NotEmpty();
        RuleFor(x => x.Tags).NotNull();
    }
}