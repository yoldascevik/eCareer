using AutoMapper;
using Company.Application.Dtos.Company;
using Company.Domain.Entities;

namespace Company.Application.Mapping
{
    public class CompanyFollowerMappingProfile: Profile
    {
        public CompanyFollowerMappingProfile()
        {
            CreateMap<CompanyFollower, CompanyFollowerDto>();
        }
    }
}