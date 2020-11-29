using System.Threading;
using System.Threading.Tasks;
using Career.MediatR.Command;
using Career.Repositories;
using Company.Application.Dtos.Company;
using Company.Application.Services.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Company.Application.Commands.Company.CreateCompany
{
    public class CreateCompanyHandler : ICommandHandler<CreateCompanyCommand, CompanyDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyService _companyService;
        private readonly ILogger<CreateCompanyHandler> _logger;

        public CreateCompanyHandler(
            IUnitOfWork unitOfWork,
            ICompanyService companyService, 
            ILogger<CreateCompanyHandler> logger) 
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _companyService = companyService;
        }

        public async Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            CompanyDto company = await _companyService.CreateCompanyAsync(request);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            _logger.LogInformation("Created new company : {CompanyId} - {CompanyName}", company.Id, company.Name);

            return company;
        }
    }
}