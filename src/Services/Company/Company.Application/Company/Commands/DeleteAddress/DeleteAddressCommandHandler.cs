using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Career.Exceptions;
using Career.MediatR.Command;
using Career.Repositories.UnitOfWok;
using Company.Application.Company.Exceptions;
using Company.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Company.Application.Company.Commands.DeleteAddress;

public class DeleteAddressCommandHandler: ICommandHandler<DeleteAddressCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICompanyRepository _companyRepository;
    private readonly ILogger<DeleteAddressCommandHandler> _logger;

    public DeleteAddressCommandHandler(IUnitOfWork unitOfWork, ICompanyRepository companyRepository, ILogger<DeleteAddressCommandHandler> logger)
    {
        Check.NotNull(companyRepository, nameof(companyRepository));
        Check.NotNull(unitOfWork, nameof(unitOfWork));
        Check.NotNull(logger, nameof(logger));
            
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetCompanyByIdAsync(request.CompanyId);
        if (company == null)
            throw new CompanyNotFoundException(request.CompanyId);

        var address = company.Addresses.FirstOrDefault(x => x.Id == request.AddressId);
        if (address == null)
            throw new AddressNotFoundException(request.AddressId);
            
        company.RemoveAddress(address);
            
        await _companyRepository.UpdateAsync(company.Id, company);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
            
        _logger.LogInformation("Address {AddressId} removed from company: {CompanyId}", request.AddressId, company.Id);
            
        return Unit.Value;
    }
}