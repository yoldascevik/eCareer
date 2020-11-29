using System.Threading;
using System.Threading.Tasks;
using Career.Exceptions.Exceptions;
using Career.MediatR.Command;
using Career.Repositories;
using Company.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Company.Application.Company.Commands.DeleteCompany
{
    public class DeleteCompanyCommandHandler: ICommandHandler<DeleteCompanyCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<DeleteCompanyCommandHandler> _logger;

        public DeleteCompanyCommandHandler(
            IUnitOfWork unitOfWork, 
            ICompanyRepository companyRepository, 
            ILogger<DeleteCompanyCommandHandler> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _companyRepository = companyRepository;
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