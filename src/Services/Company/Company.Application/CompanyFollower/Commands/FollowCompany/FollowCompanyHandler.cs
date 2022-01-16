using System.Threading;
using System.Threading.Tasks;
using Career.MediatR.Command;
using Career.Repositories.UnitOfWok;
using Company.Application.Company.Exceptions;
using Company.Application.Specifications;
using Company.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Company.Application.CompanyFollower.Commands.FollowCompany;

public class FollowCompanyHandler : ICommandHandler<FollowCompanyCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<FollowCompanyHandler> _logger;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICompanyFollowerRepository _companyFollowerRepository;

    public FollowCompanyHandler(
        IUnitOfWork unitOfWork,
        ILogger<FollowCompanyHandler> logger,
        ICompanyRepository companyRepository, 
        ICompanyFollowerRepository companyFollowerRepository)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _companyRepository = companyRepository;
        _companyFollowerRepository = companyFollowerRepository;
    }

    public async Task<Unit> Handle(FollowCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetCompanyByIdAsync(request.CompanyId);
        if (company == null)
            throw new CompanyNotFoundException(request.CompanyId);
            
        company.Follow(request.UserId, new CompanyFollowerUniquenessSpecification(_companyFollowerRepository));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
            
        _logger.LogInformation("User {UserId} followed company {CompanyId})", request.UserId, request.CompanyId);
            
        return Unit.Value;
    }
}