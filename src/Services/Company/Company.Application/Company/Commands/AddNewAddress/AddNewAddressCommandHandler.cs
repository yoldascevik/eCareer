using Career.Exceptions;
using Career.MediatR.Command;
using Career.Repositories.UnitOfWok;
using Company.Application.Company.Exceptions;
using Company.Domain.Entities;
using Company.Domain.Refs;
using Company.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Company.Application.Company.Commands.AddNewAddress;

public class AddNewAddressCommandHandler : ICommandHandler<AddNewAddressCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICityRefRepository _cityRefRepository;
    private readonly ICountryRefRepository _countryRefRepository;
    private readonly IDistrictRefRepository _districtRefRepository;
    private readonly ILogger<AddNewAddressCommandHandler> _logger;

    public AddNewAddressCommandHandler(
        IUnitOfWork unitOfWork,
        ICompanyRepository companyRepository,
        ICityRefRepository cityRefRepository,
        ICountryRefRepository countryRefRepository,
        IDistrictRefRepository districtRefRepository,
        ILogger<AddNewAddressCommandHandler> logger)
    {
        Check.NotNull(companyRepository, nameof(companyRepository));
        Check.NotNull(cityRefRepository, nameof(cityRefRepository));
        Check.NotNull(countryRefRepository, nameof(countryRefRepository));
        Check.NotNull(districtRefRepository, nameof(districtRefRepository));
        Check.NotNull(unitOfWork, nameof(unitOfWork));
        Check.NotNull(logger, nameof(logger));

        _districtRefRepository = districtRefRepository;
        _countryRefRepository = countryRefRepository;
        _cityRefRepository = cityRefRepository;
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Guid> Handle(AddNewAddressCommand request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetCompanyByIdAsync(request.CompanyId);
        if (company == null)
            throw new CompanyNotFoundException(request.CompanyId);

        var cityRef = await _cityRefRepository.GetByKeyAsync(request.AddressDto.City.RefId)
                      ?? CityRef.Create(request.AddressDto.City.RefId, request.AddressDto.City.Name);

        var countryRef = await _countryRefRepository.GetByKeyAsync(request.AddressDto.Country.RefId)
                         ?? CountryRef.Create(request.AddressDto.Country.RefId, request.AddressDto.Country.Name);

        DistrictRef districtRef = null;
        if (request.AddressDto.District != null)
        {
            districtRef = await _districtRefRepository.GetByKeyAsync(request.AddressDto.District.RefId)
                          ?? DistrictRef.Create(request.AddressDto.District.RefId, request.AddressDto.District.Name);
        }

        var address = Address.Create(
            company.Id,
            request.AddressDto.Title,
            countryRef,
            cityRef,
            districtRef,
            request.AddressDto.Details,
            request.AddressDto.ZipCode,
            request.AddressDto.IsPrimary);

        company.AddAddress(address);
            
        await _companyRepository.UpdateAsync(company.Id, company);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("New address added to company: {CompanyId}", company.Id);

        return address.Id;
    }
}