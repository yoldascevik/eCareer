using AutoMapper;
using Career.Exceptions;
using Career.Exceptions.Exceptions;
using Career.MediatR.Command;
using Career.Repositories.UnitOfWok;
using Company.Application.Company.Dtos;
using Company.Application.Company.Exceptions;
using Company.Domain.Entities;
using Company.Domain.Refs;
using Company.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Company.Application.Company.Commands.UpdateAddress;

public class UpdateAddressCommandHandler : ICommandHandler<UpdateAddressCommand, AddressDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICityRefRepository _cityRefRepository;
    private readonly ICountryRefRepository _countryRefRepository;
    private readonly IDistrictRefRepository _districtRefRepository;
    private readonly ILogger<UpdateAddressCommandHandler> _logger;

    public UpdateAddressCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ICompanyRepository companyRepository,
        ICityRefRepository cityRefRepository,
        ICountryRefRepository countryRefRepository,
        IDistrictRefRepository districtRefRepository,
        ILogger<UpdateAddressCommandHandler> logger)
    {
        Check.NotNull(companyRepository, nameof(companyRepository));
        Check.NotNull(cityRefRepository, nameof(cityRefRepository));
        Check.NotNull(countryRefRepository, nameof(countryRefRepository));
        Check.NotNull(districtRefRepository, nameof(districtRefRepository));
        Check.NotNull(unitOfWork, nameof(unitOfWork));
        Check.NotNull(logger, nameof(logger));
        Check.NotNull(mapper, nameof(mapper));

        _districtRefRepository = districtRefRepository;
        _countryRefRepository = countryRefRepository;
        _cityRefRepository = cityRefRepository;
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<AddressDto> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetCompanyByIdAsync(request.CompanyId);
        if (company == null)
            throw new CompanyNotFoundException(request.CompanyId);

        var address = company.Addresses.FirstOrDefault(x => x.Id == request.AddressId);
        if (address == null)
            throw new AddressNotFoundException(request.AddressId);

        address.SetTitle(request.AddressDto.Title)
            .SetDetails(request.AddressDto.Details)
            .SetZipCode(request.AddressDto.ZipCode);

        if (request.AddressDto.IsPrimary)
        {
            company.SetPrimaryAddress(address);
        }
        else if (address.IsPrimary && !request.AddressDto.IsPrimary)
        {
            throw new BusinessException("Please mark a different address as primary.");
        }

        await SetAddressRefs(request.AddressDto, address);
        await _companyRepository.UpdateAsync(company.Id, company);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Address {AddressId} updated for company: {CompanyId}", address.Id, company.Id);

        return _mapper.Map<AddressDto>(address);
    }

    private async Task SetAddressRefs(AddressInputDto addressDto, Address address)
    {
        if (address.CountryRef.RefId != addressDto.Country.RefId)
        {
            address.SetCountry(
                await _countryRefRepository.GetByKeyAsync(addressDto.Country.RefId)
                ?? CountryRef.Create(addressDto.Country.RefId, addressDto.Country.Name)
            );
        }

        if (address.CityRef.RefId != addressDto.City.RefId)
        {
            address.SetCity(
                await _cityRefRepository.GetByKeyAsync(addressDto.City.RefId)
                ?? CityRef.Create(addressDto.City.RefId, addressDto.City.Name)
            );
        }

        if (address.DistrictRef?.RefId != addressDto.District?.RefId)
        {
            DistrictRef districtRef = null;
            if (addressDto.District != null)
            {
                districtRef = await _districtRefRepository.GetByKeyAsync(addressDto.District.RefId)
                              ?? DistrictRef.Create(addressDto.District.RefId, addressDto.District.Name);
            }

            address.SetDistrict(districtRef);
        }
    }
}