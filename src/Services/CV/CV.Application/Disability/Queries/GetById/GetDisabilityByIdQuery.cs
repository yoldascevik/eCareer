using Career.MediatR.Query;
using CurriculumVitae.Application.Disability.Dtos;

namespace CurriculumVitae.Application.Disability.Queries.GetById
{
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
}