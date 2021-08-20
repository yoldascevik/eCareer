using Career.MediatR.Command;
using CurriculumVitae.Application.WorkExperience.Dtos;

namespace CurriculumVitae.Application.WorkExperience.Commands.Add
{
    public class AddWorkExperienceCommand : ICommand<WorkExperienceDto>
    {
        public AddWorkExperienceCommand(string cvId, WorkExperienceInputDto workExperience)
        {
            CvId = cvId;
            WorkExperience = workExperience;
        }

        public string CvId { get; }
        public WorkExperienceInputDto WorkExperience { get; }
    }
}