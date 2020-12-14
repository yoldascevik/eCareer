using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Bogus;
using Career.Exceptions.Exceptions;
using Career.Repositories;
using Company.Application.Company.Commands.UpdateCompanyAddress;
using Company.Application.Company.Dtos;
using Company.Domain.Repositories;
using Company.Domain.Values;
using Company.Tests.Helpers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Company.Tests.IntegrationTests.Company
{
    public class UpdateCompanyAddressTests
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<UpdateCompanyAddressHandler> _logger;

        public UpdateCompanyAddressTests()
        {
            _mapper = Substitute.For<IMapper>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _companyRepository = Substitute.For<ICompanyRepository>();
            _logger = Substitute.For<ILogger<UpdateCompanyAddressHandler>>();
        }

        [Fact]
        public async Task UpdateCompanyAddressHandler_ShouldReturnNewAddress_WhenAddressChanged()
        {
            // Arrange
            var company = CompanyFaker.CreateFakeCompany();
            var updateAddressCommand = GetCommand(company.Id);
            var commandHandler = new UpdateCompanyAddressHandler(_mapper, _unitOfWork, _companyRepository, _logger);

            _companyRepository.GetCompanyByIdAsync(updateAddressCommand.CompanyId).Returns(company);
            _mapper.Map<AddressDto>(Arg.Any<AddressInfo>()).Returns(updateAddressCommand.Address);

            // Act
            AddressDto result = await commandHandler.Handle(updateAddressCommand, CancellationToken.None);

            // Assert
            await _unitOfWork.Received().SaveChangesAsync();
            Assert.Equal(updateAddressCommand.Address, result);
            Assert.Equal(updateAddressCommand.Address.CountryId, company.AddressInfo.CountryId);
            Assert.Equal(updateAddressCommand.Address.CityId, company.AddressInfo.CityId);
            Assert.Equal(updateAddressCommand.Address.DistrictId, company.AddressInfo.DistrictId);
            Assert.Equal(updateAddressCommand.Address.Address, company.AddressInfo.Address);
        }
        
        [Fact]
        public async Task UpdateCompanyAddressHandler_ThrowNotFoundException_WhenCompanyNotExists()
        {
            // Arrange
            var company = CompanyFaker.CreateFakeCompany();
            var updateAddressCommand = GetCommand(company.Id);
            var commandHandler = new UpdateCompanyAddressHandler(_mapper, _unitOfWork, _companyRepository, _logger);

            _companyRepository.GetCompanyByIdAsync(company.Id).ReturnsNull();

            // Act
            var actualException = await Assert.ThrowsAsync<NotFoundException>(() => commandHandler.Handle(updateAddressCommand, CancellationToken.None));

            // Assert
            Assert.Equal($"Company is not found by id: {company.Id}", actualException.Message);
        }

        private UpdateCompanyAddressCommand GetCommand(Guid companyId)
        {
            var faker = new Faker();
            var addressDto = new AddressDto
            {
                CountryId = faker.Random.Guid().ToString(),
                CityId = faker.Random.Guid().ToString(),
                DistrictId = faker.Random.Guid().ToString(),
                Address = faker.Address.FullAddress()
            };

            return new UpdateCompanyAddressCommand(companyId, addressDto);
        }
    }
}