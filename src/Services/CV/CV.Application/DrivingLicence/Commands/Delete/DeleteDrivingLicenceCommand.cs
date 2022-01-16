using Career.MediatR.Command;

namespace CurriculumVitae.Application.DrivingLicence.Commands.Delete;

public class DeleteDrivingLicenceCommand : ICommand
{
    public DeleteDrivingLicenceCommand(string cvId, string drivingLicenceId)
    {
        CvId = cvId;
        DrivingLicenceId = drivingLicenceId;
    }

    public string CvId { get; }
    public string DrivingLicenceId { get; }
}