using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Repositories.UnitOfWok;
using Company.Application.Company.Commands.UpdateAddress;
using Company.Application.Company.Exceptions;
using Company.Domain.Repositories;
using Company.Tests.Helpers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Company.Tests.IntegrationTests.Company
{
    public class UpdateAddressTests
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICityRefRepository _cityRefRepository;
        private readonly ICountryRefRepository _countryRefRepository;
        private readonly IDistrictRefRepository _districtRefRepository;
        private readonly ILogger<UpdateAddressCommandHandler> _logger;

        public UpdateAddressTests()
        {
            _mapper = Substitute.For<IMapper>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _companyRepository = Substitute.For<ICompanyRepository>();
            _cityRefRepository = Substitute.For<ICityRefRepository>();
            _countryRefRepository = Substitute.For<ICountryRefRepository>();
            _districtRefRepository = Substitute.For<IDistrictRefRepository>();
            _logger = Substitute.For<ILogger<UpdateAddressCommandHandler>>();
        }

        [Fact]
        public async Task UpdateAddress_ShouldAddressUpdated_WhenSuccess()
        {
            // Arrange
            var company = CompanyFaker.CreateFakeCompany();
            var companyAddress = AddressFaker.GenerateCompanyAddress(company.Id);
            var command = new UpdateAddressCommand(company.Id, companyAddress.Id, AddressFaker.GenerateAddressInputDto());
            var commandHandler = new UpdateAddressCommandHandler(_mapper, _unitOfWork, _companyRepository, 
                _cityRefRepository,_countryRefRepository,_districtRefRepository,_logger);
            
            company.AddAddress(AddressFaker.GenerateCompanyAddress(company.Id)); //add primary address
            company.AddAddress(companyAddress);

            _companyRepository.GetCompanyByIdAsync(company.Id).Returns(company);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(command.AddressDto.Title, companyAddress.Title);
            Assert.Equal(command.AddressDto.Country.RefId, companyAddress.CountryRef.RefId);
            await _companyRepository.Received().UpdateAsync(company.Id, company);
        }
        
        [Fact]
        public async Task UpdateAddress_ShouldBeLogInformation_WhenSuccess()
        {
            // Arrange
            var company = CompanyFaker.CreateFakeCompany();
            var companyAddress = AddressFaker.GenerateCompanyAddress(company.Id);
            var command = new UpdateAddressCommand(company.Id, companyAddress.Id, AddressFaker.GenerateAddressInputDto());
            var commandHandler = new UpdateAddressCommandHandler(_mapper, _unitOfWork, _companyRepository, 
                _cityRefRepository,_countryRefRepository,_districtRefRepository,_logger);

            company.AddAddress(AddressFaker.GenerateCompanyAddress(company.Id)); //add primary address
            company.AddAddress(companyAddress);
            
            _companyRepository.GetCompanyByIdAsync(company.Id).Returns(company);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            _logger.ReceivedWithAnyArgs().LogInformation("");
        }
        
        [Fact]
        public async Task UpdateAddress_ThrowCompanyNotFoundException_WhenCompanyNotExists()
        {
            // Arrange
            var company = CompanyFaker.CreateFakeCompany();
            var command = new UpdateAddressCommand(company.Id, Guid.NewGuid(), AddressFaker.GenerateAddressInputDto());
            var commandHandler = new UpdateAddressCommandHandler(_mapper, _unitOfWork, _companyRepository, 
                _cityRefRepository,_countryRefRepository,_districtRefRepository,_logger);

            _companyRepository.GetCompanyByIdAsync(company.Id).ReturnsNull();

            // Act
            var actualException = await Assert.ThrowsAsync<CompanyNotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));

            // Assert
            Assert.IsType<CompanyNotFoundException>(actualException);
        }
        
        [Fact]
        public async Task UpdateAddress_ThrowAddressNotFoundException_WhenAddressNotExists()
        {
            // Arrange
            var company = CompanyFaker.CreateFakeCompany();
            var command = new UpdateAddressCommand(company.Id, Guid.NewGuid(), AddressFaker.GenerateAddressInputDto());
            var commandHandler = new UpdateAddressCommandHandler(_mapper, _unitOfWork, _companyRepository, 
                _cityRefRepository,_countryRefRepository,_districtRefRepository,_logger);

            _companyRepository.GetCompanyByIdAsync(company.Id).Returns(company);

            // Act
            var actualException = await Assert.ThrowsAsync<AddressNotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));

            // Assert
            Assert.IsType<AddressNotFoundException>(actualException);
        }
        
        [Fact]
        public async Task UpdateAddress_ShouldPrimaryPropertyOfOldPrimaryAddressFalse_WhenUpdatedNewNewPrimaryAddress()
        {
            // Arrange
            var company = CompanyFaker.CreateFakeCompany();
            var oldPrimaryAddress = AddressFaker.GenerateCompanyAddress(company.Id, isPrimary: true);
            var newPrimaryAddress = AddressFaker.GenerateCompanyAddress(company.Id, isPrimary: false);
            var commandHandler = new UpdateAddressCommandHandler(_mapper, _unitOfWork, _companyRepository, 
                _cityRefRepository,_countryRefRepository,_districtRefRepository,_logger);
            var command = new UpdateAddressCommand(company.Id, newPrimaryAddress.Id, AddressFaker.GenerateAddressInputDto());
            
            command.AddressDto.IsPrimary = true;
            company.AddAddress(oldPrimaryAddress);
            company.AddAddress(newPrimaryAddress);
            
            _companyRepository.GetCompanyByIdAsync(company.Id).Returns(company);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(newPrimaryAddress.IsPrimary);
            Assert.False(oldPrimaryAddress.IsPrimary);
            Assert.NotEqual(oldPrimaryAddress.Id, newPrimaryAddress.Id);
        }
    }
}