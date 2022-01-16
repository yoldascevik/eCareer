using System.Collections.Generic;
using Career.MediatR.Query;
using CurriculumVitae.Application.WorkExperience.Dtos;

namespace CurriculumVitae.Application.WorkExperience.Queries.Get;

public class GetWorkExperiencesQuery : IQuery<List<WorkExperienceDto>>
{
    public GetWorkExperiencesQuery(string cvId)
    {
        CvId = cvId;
    }

    public string CvId { get; }
}