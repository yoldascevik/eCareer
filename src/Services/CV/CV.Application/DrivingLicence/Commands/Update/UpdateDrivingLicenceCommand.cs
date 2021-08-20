using Career.MediatR.Command;
using CurriculumVitae.Application.DrivingLicence.Dtos;

namespace CurriculumVitae.Application.DrivingLicence.Commands.Update
{
    public class UpdateDrivingLicenceCommand : ICommand
    {
        public UpdateDrivingLicenceCommand(string cvId, string drivingLicenceId, DrivingLicenceInputDto drivingLicence)
        {
            CvId = cvId;
            DrivingLicenceId = drivingLicenceId;
            DrivingLicence = drivingLicence;
        }

        public string CvId { get; }
        public string DrivingLicenceId { get; }
        public DrivingLicenceInputDto DrivingLicence { get; }
    }
}