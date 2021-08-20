using Career.MediatR.Command;
using CurriculumVitae.Application.Education.Dtos;

namespace CurriculumVitae.Application.Education.Commands.Add
{
    public class AddEducationCommand : ICommand<EducationDto>
    {
        public AddEducationCommand(string cvId, EducationInputDto education)
        {
            CvId = cvId;
            Education = education;
        }

        public string CvId { get; }
        public EducationInputDto Education { get; }
    }
}