using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Exceptions.Exceptions;
using Career.MediatR.Command;
using Career.Repositories;
using Company.Application.Company.Dtos;
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

        public UpdateCompanyDetailsHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<UpdateCompanyDetailsHandler> logger,
            ICompanyRepository companyRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _companyRepository = companyRepository;
        }

        public async Task<CompanyDetailDto> Handle(UpdateCompanyDetailsCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(request.CompanyId);
            if (company == null)
                throw new ItemNotFoundException($"Company is not found by id: {request.CompanyId}");
            
            company.UpdateDetails(
                request.Company.Phone, 
                request.Company.MobilePhone, 
                request.Company.FaxNumber, 
                request.Company.Website, 
                request.Company.EmployeesCount, 
                request.Company.EstablishedYear, 
                request.Company.SectorId);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            _logger.LogInformation("Company detail info updated : {CompanyId} - {Info}", request.CompanyId , request.Company);
            
            return _mapper.Map<CompanyDetailDto>(company);
        }
    }
}