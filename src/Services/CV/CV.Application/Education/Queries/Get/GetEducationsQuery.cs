using System.Collections.Generic;
using Career.MediatR.Query;
using CurriculumVitae.Application.Education.Dtos;

namespace CurriculumVitae.Application.Education.Queries.Get;

public class GetEducationsQuery : IQuery<List<EducationDto>>
{
    public GetEducationsQuery(string cvId)
    {
        CvId = cvId;
    }

    public string CvId { get; }
}