using AutoMapper;
using Company.Application.CompanyFollower.Commands.FollowCompany;

namespace Company.Application.CompanyFollower;

public class CompanyFollowerMappingProfile: Profile
{
    public CompanyFollowerMappingProfile()
    {
        CreateMap<FollowCompanyCommand, Domain.Entities.CompanyFollower>();
    }
}