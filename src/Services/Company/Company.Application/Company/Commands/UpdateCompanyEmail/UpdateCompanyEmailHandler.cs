using System.Threading;
using System.Threading.Tasks;
using Career.MediatR.Command;
using Career.Repositories.UnitOfWok;
using Company.Application.Company.Exceptions;
using Company.Application.Specifications;
using Company.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Company.Application.Company.Commands.UpdateCompanyEmail
{
    public class UpdateCompanyEmailHandler : ICommandHandler<UpdateCompanyEmailCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<UpdateCompanyEmailHandler> _logger;

        public UpdateCompanyEmailHandler(
            IUnitOfWork unitOfWork,
            ICompanyRepository companyRepository,
            ILogger<UpdateCompanyEmailHandler> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _companyRepository = companyRepository;
        }

        public async Task<Unit> Handle(UpdateCompanyEmailCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(request.CompanyId);
            if (company == null)
                throw new CompanyNotFoundException(request.CompanyId);
            
            company.UpdateEmailAddress(request.Email, new EmailAddressUniquenessSpecification(_companyRepository, company.Id));
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            _logger.LogInformation("Company email address changed : {CompanyId} - {Email}", request.CompanyId, request.Email);

            return Unit.Value;
        }
    }
}