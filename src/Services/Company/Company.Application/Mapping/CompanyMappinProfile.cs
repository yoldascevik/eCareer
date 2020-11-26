using AutoMapper;
using Company.Application.Commands.Company.CreateCompany;
using Company.Application.Commands.Company.UpdateCompany;
using Company.Application.Dtos.Company;

namespace Company.Application.Mapping
{
    public class CompanyMappinProfile: Profile
    {
        public CompanyMappinProfile()
        {
            CreateMap<Domain.Entities.Company, CompanyDto>();
            CreateMap<CreateCompanyCommand, Domain.Entities.Company>();
            CreateMap<UpdateCompanyCommmand, Domain.Entities.Company>();
        }
    }
}