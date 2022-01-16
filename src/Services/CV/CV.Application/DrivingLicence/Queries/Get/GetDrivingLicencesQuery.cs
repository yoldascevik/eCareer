using System.Collections.Generic;
using Career.MediatR.Query;
using CurriculumVitae.Application.DrivingLicence.Dtos;

namespace CurriculumVitae.Application.DrivingLicence.Queries.Get;

public class GetDrivingLicencesQuery : IQuery<List<DrivingLicenceDto>>
{
    public GetDrivingLicencesQuery(string cvId)
    {
        CvId = cvId;
    }

    public string CvId { get; }
}