using Career.MediatR.Command;
using CurriculumVitae.Application.PersonalInfo.Dtos;

namespace CurriculumVitae.Application.PersonalInfo.Commands.UpdateDisability;

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