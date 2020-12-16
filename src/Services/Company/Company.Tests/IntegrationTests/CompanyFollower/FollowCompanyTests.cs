using System;
using System.Threading;
using System.Threading.Tasks;
using Career.Domain.BusinessRule;
using Career.Exceptions.Exceptions;
using Career.Repositories;
using Company.Application.CompanyFollower.Commands.FollowCompany;
using Company.Domain.Repositories;
using Company.Domain.Rules.CompanyFollower;
using Company.Tests.Helpers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Company.Tests.IntegrationTests.CompanyFollower
{
    public class FollowCompanyTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<FollowCompanyHandler> _logger;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyFollowerRepository _companyFollowerRepository;

        public FollowCompanyTests()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _logger = Substitute.For<ILogger<FollowCompanyHandler>>();
            _companyRepository = Substitute.For<ICompanyRepository>();
            _companyFollowerRepository = Substitute.For<ICompanyFollowerRepository>();
        }

        [Fact]
        public async Task FollowCompanyHandler_ShouldUserAddedToCompanyFollowers_WhenSuccessful()
        {
            // Arrange
            var company = CompanyFaker.CreateFakeCompany();
            var command = new FollowCompanyCommand(Guid.NewGuid(), company.Id);
            var commandHandler = new FollowCompanyHandler(_unitOfWork, _logger, _companyRepository, _companyFollowerRepository);

            _companyRepository.GetCompanyByIdAsync(command.CompanyId).Returns(company);
            
            // Act
            await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            await _unitOfWork.Received().SaveChangesAsync();
            Assert.Contains(company.Followers, c => c.UserId == command.UserId);
        }
        
        [Fact]
        public async Task FollowCompanyHandler_ThrowNotFoundException_WhenCompanyNotExists()
        {
            // Arrange
            var command = new FollowCompanyCommand(Guid.NewGuid(), Guid.NewGuid());
            var commandHandler = new FollowCompanyHandler(_unitOfWork, _logger, _companyRepository, _companyFollowerRepository);
            
            _companyRepository.GetCompanyByIdAsync(command.CompanyId).ReturnsNull();
        
            // Act
            var actualException = await Assert.ThrowsAsync<NotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));
        
            // Assert
            Assert.Equal($"Company is not found by id: {command.CompanyId}", actualException.Message);
        }
        
        [Fact]
        public async Task FollowCompanyHandler_ThrowBusinessRuleValidationException_For_CompanyFollowerMustBeUniqueRule_WhenFollowerAlreadyExists()
        {
            // Arrange
            var company = CompanyFaker.CreateFakeCompany();
            var command = new FollowCompanyCommand(Guid.NewGuid(), company.Id);
            var commandHandler = new FollowCompanyHandler(_unitOfWork, _logger, _companyRepository, _companyFollowerRepository);

            _companyRepository.GetCompanyByIdAsync(command.CompanyId).Returns(company);
            _companyFollowerRepository.CheckUserExistsInCompanyFollowers(command.CompanyId, command.UserId).Returns(true);

            // Act
            var actualException = await Assert.ThrowsAsync<BusinessRuleValidationException>(() => commandHandler.Handle(command, CancellationToken.None));

            // Assert
            Assert.Equal(typeof(CompanyFollowerMustBeUniqueRule), actualException.BrokenRule.GetType());
        }
        
    }
}