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
using Company.Application.Company.Commands.UpdateCompanyTaxInfo;
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
    public class UpdateCompanyTaxInfoTests
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<UpdateCompanyTaxInfoHandler> _logger;

        public UpdateCompanyTaxInfoTests()
        {
            _mapper = Substitute.For<IMapper>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _companyRepository = Substitute.For<ICompanyRepository>();
            _logger = Substitute.For<ILogger<UpdateCompanyTaxInfoHandler>>();
        }

        [Fact]
        public async Task UpdateCompanyTaxInfoHandler_ShouldReturnExpectedTaxDto_WhenCompanyTaxInfoChanged()
        {
            // Arrange
            var company = CompanyFaker.CreateFakeCompany();
            var command = GetCommand(company.Id);
            var commandHandler = new UpdateCompanyTaxInfoHandler(_mapper, _unitOfWork, _companyRepository, _logger);

            _companyRepository.GetCompanyByIdAsync(command.CompanyId).Returns(company);
            _mapper.Map<TaxDto>(Arg.Any<TaxInfo>()).Returns(command.TaxInfo);
            
            // Act
            TaxDto result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            await _unitOfWork.Received().SaveChangesAsync();
            Assert.Equal(command.TaxInfo, result);
            Assert.Equal(command.TaxInfo.CountryId, company.TaxInfo.CountryId);
            Assert.Equal(command.TaxInfo.TaxNumber, company.TaxInfo.TaxNumber);
            Assert.Equal(command.TaxInfo.TaxOffice, company.TaxInfo.TaxOffice);
        }
        
        [Fact]
        public async Task UpdateCompanyTaxInfoHandler_ThrowNotFoundException_WhenCompanyNotExists()
        {
            // Arrange
            var company = CompanyFaker.CreateFakeCompany();
            var command = GetCommand(company.Id);
            var commandHandler = new UpdateCompanyTaxInfoHandler(_mapper, _unitOfWork, _companyRepository, _logger);
            
            _companyRepository.GetCompanyByIdAsync(company.Id).ReturnsNull();
        
            // Act
            var actualException = await Assert.ThrowsAsync<NotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));
        
            // Assert
            Assert.Equal($"Company is not found by id: {company.Id}", actualException.Message);
        }
        
        [Fact]
        public async Task UpdateCompanyTaxInfoHandler_ThrowBusinessRuleValidationException_For_TaxNumberMustBeUniqueRule_WhenTaxNumberExists()
        {
            // Arrange
            var company = CompanyFaker.CreateFakeCompany();
            var command = GetCommand(company.Id);
            var commandHandler = new UpdateCompanyTaxInfoHandler(_mapper, _unitOfWork, _companyRepository, _logger);
            
            _companyRepository.GetCompanyByIdAsync(command.CompanyId).Returns(company);
            _companyRepository.IsTaxNumberExistsAsync(command.TaxInfo.TaxNumber, command.TaxInfo.CountryId, command.CompanyId).Returns(true);

            // Act
            var actualException = await Assert.ThrowsAsync<BusinessRuleValidationException>(() => commandHandler.Handle(command, CancellationToken.None));

            // Assert
            Assert.Equal(typeof(TaxNumberMustBeUniqueRule), actualException.BrokenRule.GetType());
        }
        
        private UpdateCompanyTaxInfoCommand GetCommand(Guid companyId)
        {
            var faker = new Faker();
            var taxDto = new TaxDto
            {
                CountryId = faker.Random.Guid().ToString(),
                TaxNumber = faker.Company.TaxNumber(),
                TaxOffice = faker.Address.City()
            };

            return new UpdateCompanyTaxInfoCommand(companyId, taxDto);
        }
    }
}