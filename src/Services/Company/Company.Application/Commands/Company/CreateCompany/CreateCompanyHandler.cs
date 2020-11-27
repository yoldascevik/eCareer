using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Repositories;
using Company.Application.Dtos.Company;
using Company.Domain.Repository;
using Company.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Company.Application.Commands.Company.CreateCompany
{
    public class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, CompanyDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateCompanyHandler> _logger;
        private readonly ICompanyRepository _companyRepository;

        public CreateCompanyHandler(
            IMapper mapper, 
            IUnitOfWork unitOfWork,
            ICompanyRepository companyRepository, 
            ILogger<CreateCompanyHandler> logger) 
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _companyRepository = companyRepository;
        }

        public async Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = _mapper.Map<Domain.Entities.Company>(request);
            var createdCompany = await _companyRepository.AddAsync(company);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Created new company : {CompanyId} - {CompanyName}", company.Id, company.Name);

            return _mapper.Map<CompanyDto>(createdCompany);
        }
    }
}