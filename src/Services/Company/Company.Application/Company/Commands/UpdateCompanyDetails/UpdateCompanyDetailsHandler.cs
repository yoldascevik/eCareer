using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Exceptions.Exceptions;
using Career.MediatR.Command;
using Career.Repositories.UnitOfWok;
using Company.Application.Company.Dtos;
using Company.Domain.Refs;
using Company.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Company.Application.Company.Commands.UpdateCompanyDetails
{
    public class UpdateCompanyDetailsHandler : ICommandHandler<UpdateCompanyDetailsCommand, CompanyDetailDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateCompanyDetailsHandler> _logger;
        private readonly ICompanyRepository _companyRepository;
        private readonly ISectorRefRepository _sectorRefRepository;

        public UpdateCompanyDetailsHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICompanyRepository companyRepository,
            ISectorRefRepository sectorRefRepository,
            ILogger<UpdateCompanyDetailsHandler> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _companyRepository = companyRepository;
            _sectorRefRepository = sectorRefRepository;
        }

        public async Task<CompanyDetailDto> Handle(UpdateCompanyDetailsCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(request.CompanyId);
            if (company == null)
                throw new NotFoundException($"Company is not found by id: {request.CompanyId}");

            var sector = await _sectorRefRepository.GetByKeyAsync(request.Company.Sector.RefId) 
                         ?? SectorRef.Create(request.Company.Sector.RefId, request.Company.Sector.Name);

            company.UpdateDetails(
                request.Company.Phone,
                request.Company.MobilePhone,
                request.Company.FaxNumber,
                request.Company.Website,
                request.Company.EmployeesCount,
                request.Company.EstablishedYear,
                sector);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Company detail info updated : {CompanyId}", request.CompanyId);

            return _mapper.Map<CompanyDetailDto>(company);
        }
    }
}