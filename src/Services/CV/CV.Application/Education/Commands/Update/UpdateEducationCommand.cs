using Career.MediatR.Command;
using CurriculumVitae.Application.Education.Dtos;

namespace CurriculumVitae.Application.Education.Commands.Update;

public class UpdateEducationCommand : ICommand
{
    public UpdateEducationCommand(string cvId, string educationId, EducationInputDto education)
    {
        CvId = cvId;
        EducationId = educationId;
        Education = education;
    }

    public string CvId { get; }
    public string EducationId { get; }
    public EducationInputDto Education { get; }
}