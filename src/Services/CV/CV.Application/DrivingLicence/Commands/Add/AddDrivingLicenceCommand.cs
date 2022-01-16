using Career.MediatR.Command;
using CurriculumVitae.Application.DrivingLicence.Dtos;

namespace CurriculumVitae.Application.DrivingLicence.Commands.Add;

public class AddDrivingLicenceCommand : ICommand<DrivingLicenceDto>
{
    public AddDrivingLicenceCommand(string cvId, DrivingLicenceInputDto drivingLicence)
    {
        CvId = cvId;
        DrivingLicence = drivingLicence;
    }

    public string CvId { get; }
    public DrivingLicenceInputDto DrivingLicence { get;  }
}