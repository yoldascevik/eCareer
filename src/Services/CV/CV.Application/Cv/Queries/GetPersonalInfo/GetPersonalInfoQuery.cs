using Career.Exceptions;
using Career.MediatR.Query;
using CurriculumVitae.Application.Cv.Dtos;

namespace CurriculumVitae.Application.Cv.Queries.GetPersonalInfo
{
    public class GetPersonalInfoQuery: IQuery<PersonalInfoDto>
    {
        public GetPersonalInfoQuery(string cvId)
        {
            Check.NotNullOrEmpty(cvId, CvId);
            CvId = cvId;
        }
        
        public string CvId { get; }
    }
}