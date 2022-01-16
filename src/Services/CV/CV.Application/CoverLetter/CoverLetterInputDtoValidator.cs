using CurriculumVitae.Application.CoverLetter.Dtos;
using FluentValidation;

namespace CurriculumVitae.Application.CoverLetter;

public class CoverLetterInputDtoValidator : AbstractValidator<CoverLetterInputDto>
{
    public CoverLetterInputDtoValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Content).NotEmpty();
    }
}