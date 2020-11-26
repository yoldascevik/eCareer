using AutoMapper;
using Company.Application.Commands.CompanyFollower.FollowCompany;
using Company.Domain.Entities;

namespace Company.Application.Mapping
{
    public class CompanyFollowerMappingProfile: Profile
    {
        public CompanyFollowerMappingProfile()
        {
            CreateMap<FollowCompanyCommand, CompanyFollower>();
        }
    }
}