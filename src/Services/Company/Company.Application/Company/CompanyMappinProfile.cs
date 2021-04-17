using AutoMapper;
using Company.Application.Company.Dtos;
using Company.Domain.Refs;
using Company.Domain.ValueObjects;

namespace Company.Application.Company
{
    public class CompanyMappinProfile : Profile
    {
        public CompanyMappinProfile()
        {
            // CompanyDto
            CreateMap<Domain.Entities.Company, CompanyDto>()
                .IncludeMembers(x => x.AddressInfo, x => x.TaxInfo);
            
            CreateMap<AddressInfo, CompanyDto>();
            CreateMap<TaxInfo, CompanyDto>();

            // AddressDto
            CreateMap<AddressInfo, AddressDto>();

            // TaxDto
            CreateMap<TaxInfo, TaxDto>();
            
            // IdNameRefDto
            CreateMap<IdNameRef, IdNameRefDto>();
            
            // CompanyDetailDto
            CreateMap<Domain.Entities.Company, CompanyDetailDto>();
        }
    }
}