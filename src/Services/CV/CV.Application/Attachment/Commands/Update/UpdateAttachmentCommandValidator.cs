using FluentValidation;

namespace CurriculumVitae.Application.Attachment.Commands.Update
{
    public class UpdateAttachmentCommandValidator : AbstractValidator<UpdateAttachmentCommand>
    {
        public UpdateAttachmentCommandValidator()
        {
            RuleFor(x => x.AttachmentId).NotEmpty();
            RuleFor(x => x.Attachment).SetValidator(new AttachmentInputDtoValidator());
        }
    }
}