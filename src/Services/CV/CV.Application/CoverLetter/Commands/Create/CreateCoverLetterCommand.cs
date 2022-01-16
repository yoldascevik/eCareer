using Career.MediatR.Command;
using CurriculumVitae.Application.CoverLetter.Dtos;

namespace CurriculumVitae.Application.CoverLetter.Commands.Create;

public class CreateCoverLetterCommand : CoverLetterInputDto, ICommand<CoverLetterDto>
{
}