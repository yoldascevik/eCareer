using Bogus;
using Career.Repositories.UnitOfWok;
using Company.Application.Company.Commands.UpdateCompanyName;
using Company.Application.Company.Exceptions;
using Company.Domain.Repositories;
using Company.Tests.Helpers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Company.Tests.IntegrationTests.Company;

public class UpdateCompanyNameTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICompanyRepository _companyRepository;
    private readonly ILogger<UpdateCompanyNameHandler> _logger;

    public UpdateCompanyNameTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _companyRepository = Substitute.For<ICompanyRepository>();
        _logger = Substitute.For<ILogger<UpdateCompanyNameHandler>>();
    }

    [Fact]
    public async Task UpdateCompanyNameHandler_ShouldExpectedName_WhenCompanyNameChanged()
    {
        // Arrange
        var company = CompanyFaker.CreateFakeCompany();
        var command = GetCommand(company.Id);
        var commandHandler = new UpdateCompanyNameHandler(_unitOfWork, _companyRepository, _logger);

        _companyRepository.GetCompanyByIdAsync(command.CompanyId).Returns(company);

        // Act
        await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(command.CompanyName, company.Name);
        await _unitOfWork.Received().SaveChangesAsync();
    }
        
    [Fact]
    public async Task UpdateCompanyNameHandler_ThrowNotFoundException_WhenCompanyNotExists()
    {
        // Arrange
        var company = CompanyFaker.CreateFakeCompany();
        var command = GetCommand(company.Id);
        var commandHandler = new UpdateCompanyNameHandler(_unitOfWork, _companyRepository, _logger);
            
        _companyRepository.GetCompanyByIdAsync(company.Id).ReturnsNull();
        
        // Act
        var actualException = await Assert.ThrowsAsync<CompanyNotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));
        
        // Assert
        Assert.NotNull(actualException);
    }

    private UpdateCompanyNameCommand GetCommand(Guid companyId)
    {
        var faker = new Faker();
        var command = new UpdateCompanyNameCommand(companyId, faker.Company.CompanyName());

        return command;
    }
}