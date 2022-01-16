using Career.Exceptions;
using Career.MediatR.Query;
using CurriculumVitae.Application.PersonalInfo.Dtos;

namespace CurriculumVitae.Application.PersonalInfo.Queries.Get;

public class GetPersonalInfoQuery: IQuery<PersonalInfoDto>
{
    public GetPersonalInfoQuery(string cvId)
    {
        Check.NotNullOrEmpty(cvId, CvId);
        CvId = cvId;
    }
        
    public string CvId { get; }
}