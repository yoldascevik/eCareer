using System.Threading;
using System.Threading.Tasks;
using Career.Exceptions.Exceptions;
using Career.Repositories;
using Company.Domain.Repository;
using Company.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Company.Application.Commands.Company.DeleteCompany
{
    public class DeleteCompanyCommandHandler: IRequestHandler<DeleteCompanyCommand>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork<CompanyDbContext> _unitOfWork;
        private readonly ILogger<DeleteCompanyCommandHandler> _logger;

        public DeleteCompanyCommandHandler(
            ICompanyRepository companyRepository, 
            IUnitOfWork<CompanyDbContext> unitOfWork, 
            ILogger<DeleteCompanyCommandHandler> logger)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByKeyAsync(request.Id);
            if (company == null)
                throw new ItemNotFoundException($"Company is not found by id: {request.Id}");

            await _companyRepository.DeleteAsync(company.Id);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            _logger.LogInformation("Company is deleted : {CompanyId} - {CompanyName}", request.Id, company.Name);

            return Unit.Value;
        }
    }
}