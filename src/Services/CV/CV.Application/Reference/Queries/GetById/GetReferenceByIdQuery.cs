using Career.MediatR.Query;
using CurriculumVitae.Application.Reference.Dtos;

namespace CurriculumVitae.Application.Reference.Queries.GetById
{
    public class GetReferenceByIdQuery : IQuery<ReferenceDto>
    {
        public GetReferenceByIdQuery(string cvId, string referenceId)
        {
            CvId = cvId;
            ReferenceId = referenceId;
        }

        public string CvId { get; }
        public string ReferenceId { get; }
    }
}