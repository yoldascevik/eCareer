using System.Threading;
using System.Threading.Tasks;
using Career.Exceptions.Exceptions;
using Career.MediatR.Command;
using Career.Repositories;
using Company.Application.Specifications;
using Company.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Company.Application.CompanyFollower.Commands.FollowCompany
{
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
                throw new ItemNotFoundException($"Company is not found: {request.CompanyId}");

            company.Follow(request.UserId, new CompanyFollowerUniquenessSpecification(_companyFollowerRepository));
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            _logger.LogInformation("User {userId} followed company {companyName} ({companyId})", request.UserId, company.Name, company.Id);
            
            return Unit.Value;
        }
    }
}