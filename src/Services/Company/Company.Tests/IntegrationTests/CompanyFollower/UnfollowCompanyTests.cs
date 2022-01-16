using System;
using System.Threading;
using System.Threading.Tasks;
using Career.Exceptions.Exceptions;
using Career.Repositories.UnitOfWok;
using Company.Application.Company.Exceptions;
using Company.Application.CompanyFollower.Commands.UnfollowCompany;
using Company.Domain.Repositories;
using Company.Tests.Helpers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Company.Tests.IntegrationTests.CompanyFollower;

public class UnfollowCompanyTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UnfollowCompanyHandler> _logger;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICompanyFollowerRepository _companyFollowerRepository;

    public UnfollowCompanyTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _logger = Substitute.For<ILogger<UnfollowCompanyHandler>>();
        _companyRepository = Substitute.For<ICompanyRepository>();
        _companyFollowerRepository = Substitute.For<ICompanyFollowerRepository>();
    }

    [Fact]
    public async Task UnfollowCompanyHandler_ShouldBeFollowerIsDeletedTrue_WhenSuccessful()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        var company = CompanyFaker.CreateFakeCompany();
        var command = new UnfollowCompanyCommand(userId, company.Id);
        var companyFollower = Domain.Entities.CompanyFollower.Create(company, userId);
        var commandHandler = new UnfollowCompanyHandler(_unitOfWork, _logger, _companyRepository, _companyFollowerRepository);

        _companyRepository.GetCompanyByIdAsync(command.CompanyId).Returns(company);
        _companyFollowerRepository.GetCompanyFollower(command.CompanyId, command.UserId).Returns(companyFollower);
            
        // Act
        await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(companyFollower.IsDeleted);
        await _unitOfWork.Received().SaveChangesAsync();
    }
        
    [Fact]
    public async Task UnfollowCompanyHandler_ThrowNotFoundException_WhenCompanyNotExists()
    {
        // Arrange
        var command = new UnfollowCompanyCommand(Guid.NewGuid(), Guid.NewGuid());
        var commandHandler = new UnfollowCompanyHandler(_unitOfWork, _logger, _companyRepository, _companyFollowerRepository);
            
        _companyRepository.GetCompanyByIdAsync(command.CompanyId).ReturnsNull();
        
        // Act
        var actualException = await Assert.ThrowsAsync<CompanyNotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));
        
        // Assert
        Assert.NotNull(actualException);
    }
        
    [Fact]
    public async Task UnfollowCompanyHandler_ThrowNotFoundException_WhenCompanyFollowerNotExists()
    {
        // Arrange
        var company = CompanyFaker.CreateFakeCompany();
        var command = new UnfollowCompanyCommand(Guid.NewGuid(), company.Id);
        var commandHandler = new UnfollowCompanyHandler(_unitOfWork, _logger, _companyRepository, _companyFollowerRepository);
        var expectedExceptionMessage = $"Company follower is not found: CompanyId: {command.CompanyId} UserId: {command.UserId}";
            
        _companyRepository.GetCompanyByIdAsync(command.CompanyId).Returns(company);
        
        // Act
        var actualException = await Assert.ThrowsAsync<NotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));
        
        // Assert
        Assert.Equal(expectedExceptionMessage, actualException.Message);
    }
}