using AutoMapper;
using Company.Application.Company.CreateCompany;
using Company.Application.Dtos;

namespace Company.Application.Mapping
{
    public class CompanyMappinProfile: Profile
    {
        public CompanyMappinProfile()
        {
            CreateMap<Domain.Company, CompanyDto>();
            CreateMap<CreateCompanyCommand, Domain.Company>();
        }
    }
}