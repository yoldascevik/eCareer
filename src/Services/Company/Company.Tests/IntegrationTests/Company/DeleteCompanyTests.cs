using System.Threading;
using System.Threading.Tasks;
using Career.Exceptions.Exceptions;
using Career.Repositories.UnitOfWok;
using Company.Application.Company.Commands.DeleteCompany;
using Company.Domain.Repositories;
using Company.Tests.Helpers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Company.Tests.IntegrationTests.Company
{
    public class DeleteCompanyTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<DeleteCompanyCommandHandler> _logger;

        public DeleteCompanyTests()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _companyRepository = Substitute.For<ICompanyRepository>();
            _logger = Substitute.For<ILogger<DeleteCompanyCommandHandler>>();
        }

        [Fact]
        public async Task DeleteCompanyHandler_ShouldCompanyIsDeletedIsTrue_WhenCompanyDeleted()
        {
            // Arrange
            var company = CompanyFaker.CreateFakeCompany();
            var command = new DeleteCompanyCommand(company.Id);
            var commandHandler = new DeleteCompanyCommandHandler(_unitOfWork, _companyRepository, _logger);
            
            _companyRepository.GetCompanyByIdAsync(company.Id).Returns(company);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(company.IsDeleted);
            await _unitOfWork.Received().SaveChangesAsync();
        }
        
        [Fact]
        public async Task DeleteCompanyHandler_ThrowNotFoundException_WhenCompanyNotExists()
        {
            // Arrange
            var company = CompanyFaker.CreateFakeCompany();
            var command = new DeleteCompanyCommand(company.Id);
            var commandHandler = new DeleteCompanyCommandHandler(_unitOfWork, _companyRepository, _logger);

            _companyRepository.GetCompanyByIdAsync(company.Id).ReturnsNull();

            // Act
            var actualException = await Assert.ThrowsAsync<NotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));

            // Assert
            Assert.Equal($"Company is not found by id: {company.Id}", actualException.Message);
        }

        [Fact]
        public async Task DeleteCompanyHandler_ShouldBeLogInformation_WhenCompanyDeleted()
        {
            // Arrange
            var company = CompanyFaker.CreateFakeCompany();
            var command = new DeleteCompanyCommand(company.Id);
            var commandHandler = new DeleteCompanyCommandHandler(_unitOfWork, _companyRepository, _logger);

            _companyRepository.GetCompanyByIdAsync(company.Id).Returns(company);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(company.IsDeleted);
            _logger.ReceivedWithAnyArgs().LogInformation(string.Empty);
        }
    }
}