using AutoMapper;
using Company.Application.Company.Dtos;
using Company.Domain.Entities;
using Company.Domain.Refs;
using Company.Domain.ValueObjects;

namespace Company.Application.Company;

public class CompanyMappinProfile : Profile
{
    public CompanyMappinProfile()
    {
        // CompanyDto
        CreateMap<Domain.Entities.Company, CompanyDto>()
            .IncludeMembers(x => x.TaxInfo);
         
        CreateMap<TaxInfo, CompanyDto>();
            
        // CompanyDetailDto
        CreateMap<Domain.Entities.Company, CompanyDetailDto>();
            
        // AddressDto
        CreateMap<Address, AddressDto>()
            .ForMember(dest => dest.Country, opt => opt.MapFrom(s=> s.CountryRef))
            .ForMember(dest => dest.City, opt => opt.MapFrom(s=> s.CityRef))
            .ForMember(dest => dest.District, opt => opt.MapFrom(s=> s.DistrictRef));

        // TaxDto
        CreateMap<TaxInfo, TaxDto>();
            
        // IdNameRefDto
        CreateMap<IdNameRef, IdNameRefDto>();
    }
}