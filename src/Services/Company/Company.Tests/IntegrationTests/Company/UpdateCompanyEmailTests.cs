using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Bogus;
using Career.Domain.BusinessRule;
using Career.Exceptions.Exceptions;
using Career.Repositories;
using Company.Application.Company.Commands.UpdateCompanyAddress;
using Company.Application.Company.Commands.UpdateCompanyDetails;
using Company.Application.Company.Commands.UpdateCompanyEmail;
using Company.Application.Company.Dtos;
using Company.Domain.Repositories;
using Company.Domain.Rules.Company;
using Company.Domain.Values;
using Company.Tests.Helpers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ClearExtensions;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Company.Tests.IntegrationTests.Company
{
    public class UpdateCompanyEmailTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<UpdateCompanyEmailHandler> _logger;

        public UpdateCompanyEmailTests()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _companyRepository = Substitute.For<ICompanyRepository>();
            _logger = Substitute.For<ILogger<UpdateCompanyEmailHandler>>();
        }

        [Fact]
        public async Task UpdateCompanyEmailHandler_ShouldExpectedEmail_WhenCompanyEmailChanged()
        {
            // Arrange
            var company = CompanyFaker.CreateFakeCompany();
            var updateEmailCommand = GetCommand(company.Id);
            var commandHandler = new UpdateCompanyEmailHandler(_unitOfWork, _companyRepository, _logger);

            _companyRepository.GetCompanyByIdAsync(updateEmailCommand.CompanyId).Returns(company);

            // Act
            await commandHandler.Handle(updateEmailCommand, CancellationToken.None);

            // Assert
            await _unitOfWork.Received().SaveChangesAsync();
            Assert.Equal(updateEmailCommand.Email, company.Email);
        }
        
        [Fact]
        public async Task UpdateCompanyEmailHandler_ThrowNotFoundException_WhenCompanyNotExists()
        {
            // Arrange
            var company = CompanyFaker.CreateFakeCompany();
            var updateEmailCommand = GetCommand(company.Id);
            var commandHandler = new UpdateCompanyEmailHandler(_unitOfWork, _companyRepository, _logger);
            
            _companyRepository.GetCompanyByIdAsync(company.Id).ReturnsNull();
        
            // Act
            var actualException = await Assert.ThrowsAsync<NotFoundException>(() => commandHandler.Handle(updateEmailCommand, CancellationToken.None));
        
            // Assert
            Assert.Equal($"Company is not found by id: {company.Id}", actualException.Message);
        }
        
        [Fact]
        public async Task UpdateCompanyEmailHandler_ThrowBusinessRuleValidationException_For_EmailAddressMustBeUniqueRule_WhenEmailAlreadyRegistered()
        {
            // Arrange
            var company = CompanyFaker.CreateFakeCompany();
            var updateEmailCommand = GetCommand(company.Id);
            var commandHandler = new UpdateCompanyEmailHandler(_unitOfWork, _companyRepository, _logger);
            
            _companyRepository.GetCompanyByIdAsync(updateEmailCommand.CompanyId).Returns(company);
            _companyRepository.IsCompanyEmailExists(updateEmailCommand.Email, updateEmailCommand.CompanyId).Returns(true);

            // Act
            var actualException = await Assert.ThrowsAsync<BusinessRuleValidationException>(() => commandHandler.Handle(updateEmailCommand, CancellationToken.None));

            // Assert
            Assert.Equal(typeof(EmailAddressMustBeUniqueRule), actualException.BrokenRule.GetType());
        }

        private UpdateCompanyEmailCommand GetCommand(Guid companyId)
        {
            var faker = new Faker();
            var command = new UpdateCompanyEmailCommand(companyId, faker.Internet.Email());

            return command;
        }
    }
}