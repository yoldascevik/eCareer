using System.Threading;
using System.Threading.Tasks;
using Career.MediatR.Command;
using Career.Repositories.UnitOfWok;
using Company.Application.Company.Exceptions;
using Company.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Company.Application.Company.Commands.UpdateCompanyName
{
    public class UpdateCompanyNameHandler : ICommandHandler<UpdateCompanyNameCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<UpdateCompanyNameHandler> _logger;

        public UpdateCompanyNameHandler(
            IUnitOfWork unitOfWork,
            ICompanyRepository companyRepository,
            ILogger<UpdateCompanyNameHandler> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _companyRepository = companyRepository;
        }

        public async Task<Unit> Handle(UpdateCompanyNameCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(request.CompanyId);
            if (company == null)
                throw new CompanyNotFoundException(request.CompanyId);
            
            string oldCompanyName = company.Name;
            company.UpdateName(request.CompanyName);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Company name updated \"{OldName}\" to \"{CompanyName}\" - {CompanyId}", oldCompanyName, company.Name, company.Id);

            return Unit.Value;
        }
    }
}