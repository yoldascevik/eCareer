using Career.MediatR.Query;
using CurriculumVitae.Application.Education.Dtos;

namespace CurriculumVitae.Application.Education.Queries.GetById;

public class GetEducationByIdQuery : IQuery<EducationDto>
{
    public GetEducationByIdQuery(string cvId, string educationId)
    {
        CvId = cvId;
        EducationId = educationId;
    }

    public string CvId { get; }
    public string EducationId { get; }
}