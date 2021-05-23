using Career.Exceptions;
using Career.MediatR.Query;
using CurriculumVitae.Application.Cv.Dtos;

namespace CurriculumVitae.Application.Cv.Queries.GetById
{
    public class GetCVByIdQuery: IQuery<CVDto>
    {
        public GetCVByIdQuery(string cvId)
        {
            Check.NotNullOrEmpty(cvId, nameof(cvId));
            CvId = cvId;
        }
        
        public string CvId { get; }
    }
}