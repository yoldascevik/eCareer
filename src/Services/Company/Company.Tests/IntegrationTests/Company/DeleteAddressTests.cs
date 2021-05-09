using System;
using System.Threading;
using System.Threading.Tasks;
using Company.Application.Company.Commands.DeleteAddress;
using Company.Application.Company.Exceptions;
using Company.Domain.Repositories;
using Company.Tests.Helpers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Company.Tests.IntegrationTests.Company
{
    public class DeleteAddressTests
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<DeleteAddressCommandHandler> _logger;

        public DeleteAddressTests()
        {
            _companyRepository = Substitute.For<ICompanyRepository>();
            _logger = Substitute.For<ILogger<DeleteAddressCommandHandler>>();
        }

        [Fact]
        public async Task DeleteAddress_ShouldAddressMarkAsDeleted_WhenSuccess()
        {
            // Arrange
            var company = CompanyFaker.CreateFakeCompany();
            var companyAddress = AddressFaker.GenerateCompanyAddress(company.Id);
            var command = new DeleteAddressCommand(company.Id, companyAddress.Id);
            var commandHandler = new DeleteAddressCommandHandler(_companyRepository, _logger);
            
            company.AddAddress(companyAddress);
            _companyRepository.GetCompanyByIdAsync(company.Id).Returns(company);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(companyAddress.IsDeleted);
            await _companyRepository.Received().UpdateAsync(company.Id, company);
        }
        
        [Fact]
        public async Task DeleteAddress_ShouldBeLogInformation_WhenSuccess()
        {
            // Arrange
            var company = CompanyFaker.CreateFakeCompany();
            var companyAddress = AddressFaker.GenerateCompanyAddress(company.Id);
            var command = new DeleteAddressCommand(company.Id, companyAddress.Id);
            var commandHandler = new DeleteAddressCommandHandler(_companyRepository, _logger);
            
            company.AddAddress(companyAddress);
            _companyRepository.GetCompanyByIdAsync(company.Id).Returns(company);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            _logger.ReceivedWithAnyArgs().LogInformation("");
        }
        
        [Fact]
        public async Task DeleteAddress_ThrowCompanyNotFoundException_WhenCompanyNotExists()
        {
            // Arrange
            var company = CompanyFaker.CreateFakeCompany();
            var command = new DeleteAddressCommand(company.Id, Guid.NewGuid());
            var commandHandler = new DeleteAddressCommandHandler(_companyRepository, _logger);
            
            _companyRepository.GetCompanyByIdAsync(company.Id).ReturnsNull();

            // Act
            var actualException = await Assert.ThrowsAsync<CompanyNotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));

            // Assert
            Assert.IsType<CompanyNotFoundException>(actualException);
        }
        
        [Fact]
        public async Task DeleteAddress_ThrowAddressNotFoundException_WhenAddressNotExists()
        {
            // Arrange
            var company = CompanyFaker.CreateFakeCompany();
            var command = new DeleteAddressCommand(company.Id, Guid.NewGuid());
            var commandHandler = new DeleteAddressCommandHandler(_companyRepository, _logger);
            
            _companyRepository.GetCompanyByIdAsync(company.Id).Returns(company);

            // Act
            var actualException = await Assert.ThrowsAsync<AddressNotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));

            // Assert
            Assert.IsType<AddressNotFoundException>(actualException);
        }
    }
}