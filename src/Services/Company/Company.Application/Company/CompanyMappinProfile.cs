using AutoMapper;
using Company.Application.Company.Dtos;

namespace Company.Application.Company
{
    public class CompanyMappinProfile: Profile
    {
        public CompanyMappinProfile()
        {
            CreateMap<Domain.Entities.Company, CompanyDto>()
                .ForMember(dest => dest, opts => opts.MapFrom(src => src.AddressInfo))
                .ForMember(dest => dest, opts => opts.MapFrom(src => src.TaxInfo));

            CreateMap<Domain.Entities.Company, AddressDto>()
                .ForMember(dest => dest, opts => opts.MapFrom(src => src.AddressInfo));
            
            CreateMap<Domain.Entities.Company, AddressDto>()
                .ForMember(dest => dest, opts => opts.MapFrom(src => src.TaxInfo));
            
            CreateMap<Domain.Entities.Company, CompanyDetailDto>();
        }
    }
}