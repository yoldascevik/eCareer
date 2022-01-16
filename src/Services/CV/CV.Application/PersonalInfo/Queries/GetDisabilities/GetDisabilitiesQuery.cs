using System.Collections.Generic;
using Career.MediatR.Query;
using CurriculumVitae.Application.PersonalInfo.Dtos;

namespace CurriculumVitae.Application.PersonalInfo.Queries.GetDisabilities;

public class GetDisabilitiesQuery : IQuery<List<DisabilityDto>>
{
    public GetDisabilitiesQuery(string cvId)
    {
        CvId = cvId;
    }

    public string CvId { get; }
}