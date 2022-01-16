using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Career.Repositories.UnitOfWok;
using Company.Application.Company.Commands.AddNewAddress;
using Company.Application.Company.Exceptions;
using Company.Domain.Entities;
using Company.Domain.Repositories;
using Company.Tests.Helpers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Company.Tests.IntegrationTests.Company;

public class AddNewAddressTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICityRefRepository _cityRefRepository;
    private readonly ICountryRefRepository _countryRefRepository;
    private readonly IDistrictRefRepository _districtRefRepository;
    private readonly ILogger<AddNewAddressCommandHandler> _logger;

    public AddNewAddressTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _companyRepository = Substitute.For<ICompanyRepository>();
        _cityRefRepository = Substitute.For<ICityRefRepository>();
        _countryRefRepository = Substitute.For<ICountryRefRepository>();
        _districtRefRepository = Substitute.For<IDistrictRefRepository>();
        _logger = Substitute.For<ILogger<AddNewAddressCommandHandler>>();
    }

    [Fact]
    public async Task AddNewAddress_ShouldAddressAddedToCompany_WhenSuccess()
    {
        // Arrange
        var company = CompanyFaker.CreateFakeCompany();
        var command = new AddNewAddressCommand(company.Id, AddressFaker.GenerateAddressInputDto());
        var commandHandler = new AddNewAddressCommandHandler(_unitOfWork, _companyRepository,
            _cityRefRepository, _countryRefRepository, _districtRefRepository, _logger);

        _companyRepository.GetCompanyByIdAsync(company.Id).Returns(company);

        // Act
        var addressId = await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Contains(company.Addresses, x => x.Id == addressId);
        await _companyRepository.Received().UpdateAsync(company.Id, company);
    }

    [Fact]
    public async Task AddNewAddress_ShouldBeLogInformation_WhenSuccess()
    {
        // Arrange
        var company = CompanyFaker.CreateFakeCompany();
        var command = new AddNewAddressCommand(company.Id, AddressFaker.GenerateAddressInputDto());
        var commandHandler = new AddNewAddressCommandHandler(_unitOfWork, _companyRepository,
            _cityRefRepository, _countryRefRepository, _districtRefRepository, _logger);

        _companyRepository.GetCompanyByIdAsync(company.Id).Returns(company);

        // Act
        await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        _logger.ReceivedWithAnyArgs().LogInformation("");
    }

    [Fact]
    public async Task AddNewAddress_ThrowCompanyNotFoundException_WhenCompanyNotExists()
    {
        // Arrange
        var company = CompanyFaker.CreateFakeCompany();
        var command = new AddNewAddressCommand(company.Id, AddressFaker.GenerateAddressInputDto());
        var commandHandler = new AddNewAddressCommandHandler(_unitOfWork, _companyRepository,
            _cityRefRepository, _countryRefRepository, _districtRefRepository, _logger);

        _companyRepository.GetCompanyByIdAsync(company.Id).ReturnsNull();

        // Act
        var actualException = await Assert.ThrowsAsync<CompanyNotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));

        // Assert
        Assert.IsType<CompanyNotFoundException>(actualException);
    }

    [Fact]
    public async Task AddNewAddress_ShouldPrimaryPropertyOfOldPrimaryAddressFalse_WhenAddedNewNewPrimaryAddress()
    {
        // Arrange
        var company = CompanyFaker.CreateFakeCompany();
        var command = new AddNewAddressCommand(company.Id, AddressFaker.GenerateAddressInputDto());
        var commandHandler = new AddNewAddressCommandHandler(_unitOfWork, _companyRepository,
            _cityRefRepository, _countryRefRepository, _districtRefRepository, _logger);

        command.AddressDto.IsPrimary = true;
        _companyRepository.GetCompanyByIdAsync(company.Id).Returns(company);

        Address oldPrimaryAddress = AddressFaker.GenerateCompanyAddress(company.Id, isPrimary: true);
        company.AddAddress(oldPrimaryAddress);

        // Act
        var addressId = await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        var newPrimaryAddress = company.Addresses.Single(x => x.IsPrimary);

        Assert.False(oldPrimaryAddress.IsPrimary);
        Assert.Equal(addressId, newPrimaryAddress.Id);
    }
}