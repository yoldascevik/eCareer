using System;
using System.Threading;
using System.Threading.Tasks;
using Career.MediatR.Command;
using Career.Repositories.UnitOfWok;
using Company.Application.Specifications;
using Company.Domain.Refs;
using Company.Domain.Repositories;
using Company.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Company.Application.Company.Commands.CreateCompany;

public class CreateCompanyCommandHandler : ICommandHandler<CreateCompanyCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICompanyRepository _companyRepository;
    private readonly ISectorRefRepository _sectorRefRepository;
    private readonly ILogger<CreateCompanyCommandHandler> _logger;

    public CreateCompanyCommandHandler(
        IUnitOfWork unitOfWork,
        ICompanyRepository companyRepository,
        ISectorRefRepository sectorRefRepository,
        ILogger<CreateCompanyCommandHandler> logger)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _companyRepository = companyRepository;
        _sectorRefRepository = sectorRefRepository;
    }

    public async Task<Guid> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var taxNumberUniquenessSpec = new TaxNumberUniquenessSpecification(_companyRepository);
        var emailUniquenessSpec = new EmailAddressUniquenessSpecification(_companyRepository);

        var taxInfo = TaxInfo.Create(request.TaxInfo.TaxNumber, request.TaxInfo.TaxOffice, request.TaxInfo.TaxCountryId);
        var sector = await _sectorRefRepository.GetByKeyAsync(request.Sector.RefId) 
                     ?? SectorRef.Create(request.Sector.RefId, request.Sector.Name); 
        var company = Domain.Entities.Company.Create(request.Name, request.Email, taxInfo, 
            request.Phone, sector, taxNumberUniquenessSpec, emailUniquenessSpec);

        await _companyRepository.AddAsync(company);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Created new company : {CompanyId} - {CompanyName}", company.Id, company.Name);

        return company.Id;
    }
}