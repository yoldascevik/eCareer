using Career.MediatR.Query;
using CurriculumVitae.Application.SocialProfile.Dtos;

namespace CurriculumVitae.Application.SocialProfile.Queries.GetById;

public class GetSocialProfileByIdQuery : IQuery<SocialProfileDto>
{
    public GetSocialProfileByIdQuery(string cvId, string socialProfileId)
    {
        CvId = cvId;
        SocialProfileId = socialProfileId;
    }

    public string CvId { get; }
    public string SocialProfileId { get; }
}