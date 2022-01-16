using Career.MediatR.Query;

namespace CurriculumVitae.Application.DisabilityType.Queries.GetById;

public class GetDisabilityTypeByIdQuery : IQuery<DisabilityTypeDto>
{
    public GetDisabilityTypeByIdQuery(string disabilityTypeId)
    {
        DisabilityTypeId = disabilityTypeId;
    }
        
    public string DisabilityTypeId { get; }
}