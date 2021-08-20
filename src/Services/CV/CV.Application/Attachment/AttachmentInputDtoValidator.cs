using CurriculumVitae.Application.Attachment.Dtos;
using FluentValidation;

namespace CurriculumVitae.Application.Attachment
{
    public class AttachmentInputDtoValidator : AbstractValidator<AttachmentInputDto>
    {
        public AttachmentInputDtoValidator()
        {
            RuleFor(x => x.Description).NotEmpty().MinimumLength(3);
            RuleFor(x => x.FileName).NotEmpty().MinimumLength(3);
            RuleFor(x => x.FileExtension).NotEmpty();
            RuleFor(x => x.MimeType).NotEmpty();
            RuleFor(x => x.SourceUrl).NotEmpty();
        }
    }
}