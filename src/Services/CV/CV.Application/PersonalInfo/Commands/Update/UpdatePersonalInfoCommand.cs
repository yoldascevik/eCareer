using Career.MediatR.Command;
using CurriculumVitae.Application.PersonalInfo.Dtos;

namespace CurriculumVitae.Application.PersonalInfo.Commands.Update
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