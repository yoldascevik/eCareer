using FluentValidation;

namespace Job.Application.Tag.Commands.Create;

public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
    }
}