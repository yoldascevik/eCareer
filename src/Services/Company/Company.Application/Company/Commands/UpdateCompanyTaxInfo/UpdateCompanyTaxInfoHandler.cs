using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Exceptions.Exceptions;
using Career.MediatR.Command;
using Career.Repositories.UnitOfWok;
using Company.Application.Company.Dtos;
using Company.Application.Specifications;
using Company.Domain.Repositories;
using Company.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Company.Application.Company.Commands.UpdateCompanyTaxInfo
{
    public class UpdateCompanyTaxInfoHandler : ICommandHandler<UpdateCompanyTaxInfoCommand, TaxDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<UpdateCompanyTaxInfoHandler> _logger;

        public UpdateCompanyTaxInfoHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICompanyRepository companyRepository,
            ILogger<UpdateCompanyTaxInfoHandler> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _companyRepository = companyRepository;
        }

        public async Task<TaxDto> Handle(UpdateCompanyTaxInfoCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(request.CompanyId);
            if (company == null)
                throw new NotFoundException($"Company is not found by id: {request.CompanyId}");

            var taxInfo = TaxInfo.Create(request.TaxInfo.TaxNumber, request.TaxInfo.TaxOffice, request.TaxInfo.CountryId);

            company.UpdateTaxInfo(taxInfo, new TaxNumberUniquenessSpecification(_companyRepository, company.Id));
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Company tax info updated : {CompanyId} - {@TaxInfo}", company.Id, company.TaxInfo);

            return _mapper.Map<TaxDto>(company.TaxInfo);
        }
    }
}