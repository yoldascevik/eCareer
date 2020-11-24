using AutoMapper;
using Company.Application.Commands.CreateCompany;
using Company.Application.Commands.UpdateCompany;
using Company.Application.Dtos;

namespace Company.Application
{
    public class CompanyMappinProfile: Profile
    {
        public CompanyMappinProfile()
        {
            CreateMap<Domain.Company, CompanyDto>();
            CreateMap<CreateCompanyCommand, Domain.Company>();
            CreateMap<UpdateCompanyCommand, Domain.Company>();
            CreateMap<CompanyCommandModel, UpdateCompanyCommand>();
        }
    }
}