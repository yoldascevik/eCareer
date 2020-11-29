using AutoMapper;
using Company.Application.Company.Commands.CreateCompany;

namespace Company.Application.Company
{
    public class CompanyMappinProfile: Profile
    {
        public CompanyMappinProfile()
        {
            CreateMap<Domain.Entities.Company, CompanyDto>();
            CreateMap<CreateCompanyCommand, Domain.Entities.Company>();
            CreateMap<CompanyRequest, Domain.Entities.Company>();
        }
    }
}