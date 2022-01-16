using System.Threading;
using System.Threading.Tasks;
using Career.MediatR.Command;
using Career.Repositories.UnitOfWok;
using Company.Application.Company.Exceptions;
using Company.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Company.Application.Company.Commands.DeleteCompany;

public class DeleteCompanyCommandHandler : ICommandHandler<DeleteCompanyCommand>
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
        var company = await _companyRepository.GetCompanyByIdAsync(request.CompanyId);
        if (company == null)
            throw new CompanyNotFoundException(request.CompanyId);

        company.MarkDeleted();
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Company is deleted : {CompanyId}", company.Id);

        return Unit.Value;
    }
}