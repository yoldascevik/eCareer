using Career.MediatR.Query;
using CurriculumVitae.Application.SocialProfileType.Dtos;

namespace CurriculumVitae.Application.SocialProfileType.Queries.GetById
{
    public class GetSocialProfileTypeByIdQuery : IQuery<SocialProfileTypeDto>
    {
        public GetSocialProfileTypeByIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}