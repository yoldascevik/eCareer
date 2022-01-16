using Career.MediatR.Query;
using CurriculumVitae.Application.DrivingLicence.Dtos;

namespace CurriculumVitae.Application.DrivingLicence.Queries.GetById;

public class GetDrivingLicenceByIdQuery : IQuery<DrivingLicenceDto>
{
    public GetDrivingLicenceByIdQuery(string cvId, string drivingLicenceId)
    {
        CvId = cvId;
        DrivingLicenceId = drivingLicenceId;
    }

    public string CvId { get; }
    public string DrivingLicenceId { get; }
}