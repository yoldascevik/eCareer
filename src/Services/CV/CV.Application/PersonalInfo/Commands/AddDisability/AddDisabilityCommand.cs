using Career.MediatR.Command;
using CurriculumVitae.Application.PersonalInfo.Dtos;

namespace CurriculumVitae.Application.PersonalInfo.Commands.AddDisability
{
    public class AddDisabilityCommand : ICommand<DisabilityDto>
    {
        public AddDisabilityCommand(string cvId, DisabilityInputDto disabilityInfo)
        {
            CvId = cvId;
            DisabilityInfo = disabilityInfo;
        }

        public string CvId { get; }
        public DisabilityInputDto DisabilityInfo { get; }
    }
}