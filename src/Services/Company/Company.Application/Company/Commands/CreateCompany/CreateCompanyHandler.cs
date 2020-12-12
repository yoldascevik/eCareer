using System;
using System.Threading;
using System.Threading.Tasks;
using Career.MediatR.Command;
using Career.Repositories;
using Company.Application.Specifications;
using Company.Domain.Repositories;
using Company.Domain.Values;
using Microsoft.Extensions.Logging;

namespace Company.Application.Company.Commands.CreateCompany
{
    public class CreateCompanyHandler : ICommandHandler<CreateCompanyCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateCompanyHandler> _logger;
        private readonly ICompanyRepository _companyRepository;

        public CreateCompanyHandler(
            IUnitOfWork unitOfWork,
            ICompanyRepository companyRepository,
            ILogger<CreateCompanyHandler> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _companyRepository = companyRepository;
        }

        public async Task<Guid> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var taxNumberUniquenessSpec = new TaxNumberUniquenessSpecification(_companyRepository);
            var emailUniquenessSpec = new EmailAddressUniquenessSpecification(_companyRepository);

            var taxInfo = TaxInfo.Create(request.TaxNumber, request.TaxOffice, request.CountryId);
            var address = AddressInfo.Create(request.CountryId, request.CityId, request.DistrictId, request.Address);
            var company = Domain.Entities.Company.Create(request.Name, request.Email, taxInfo, address, request.Website,
                request.Phone, request.MobilePhone, request.FaxNumber, request.EmployeesCount, request.EstablishedYear,
                request.SectorId, taxNumberUniquenessSpec, emailUniquenessSpec);

            await _companyRepository.AddAsync(company);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Created new company : {CompanyId} - {CompanyName}", company.Id, company.Name);

            return company.Id;
        }
    }
}