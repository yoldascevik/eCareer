using Career.MediatR.Command;
using CurriculumVitae.Application.Disability.Dtos;

namespace CurriculumVitae.Application.Disability.Commands.Update
{
    public class UpdateDisabilityCommand : ICommand
    {
        public UpdateDisabilityCommand(string cvId, string disabilityId, DisabilityInputDto disabilityInfo)
        {
            CvId = cvId;
            DisabilityInfo = disabilityInfo;
            DisabilityId = disabilityId;
        }

        public string CvId { get; }
        public string DisabilityId { get; }
        public DisabilityInputDto DisabilityInfo { get; }
    }
}