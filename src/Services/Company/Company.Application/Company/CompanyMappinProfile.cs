using AutoMapper;
using Company.Application.Company.Dtos;
using Company.Domain.ValueObjects;
using Company.Domain.Values;

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
            
            // IdNameLookupDto
            CreateMap<IdNameLookup, IdNameLookupDto>();
            
            // CompanyDetailDto
            CreateMap<Domain.Entities.Company, CompanyDetailDto>();
        }
    }
}