using Career.MediatR.Query;
using CurriculumVitae.Application.PersonalInfo.Dtos;

namespace CurriculumVitae.Application.PersonalInfo.Queries.GetDisabilityById;

public class GetDisabilityByIdQuery : IQuery<DisabilityDto>
{
    public GetDisabilityByIdQuery(string cvId, string disabilityId)
    {
        CvId = cvId;
        DisabilityId = disabilityId;
    }

    public string CvId { get; }
    public string DisabilityId { get; }
}