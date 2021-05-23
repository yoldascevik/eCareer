using Career.MediatR.Command;
using CurriculumVitae.Application.Cv.Dtos;

namespace CurriculumVitae.Application.Cv.Commands.UpdatePersonalInfo
{
    public class UpdatePersonalInfoCommand : ICommand
    {
        public UpdatePersonalInfoCommand(string cvId, PersonalInfoDto personalInfo)
        {
            CvId = cvId;
            PersonalInfo = personalInfo;
        }

        public string CvId { get; }
        public PersonalInfoDto PersonalInfo { get; }
    }
}