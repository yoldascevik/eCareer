using System.Collections.Generic;
using Career.MediatR.Query;
using CurriculumVitae.Application.SocialProfile.Dtos;

namespace CurriculumVitae.Application.SocialProfile.Queries.Get;

public class GetSocialProfilesQuery : IQuery<List<SocialProfileDto>>
{
    public GetSocialProfilesQuery(string cvId)
    {
        CvId = cvId;
    }

    public string CvId { get; }
}