using System.Threading;
using System.Threading.Tasks;
using Career.Exceptions.Exceptions;
using Career.MediatR.Command;
using Career.Repositories.UnitOfWok;
using Company.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Company.Application.CompanyFollower.Commands.UnfollowCompany
{
    public class UnfollowCompanyHandler: ICommandHandler<UnfollowCompanyCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UnfollowCompanyHandler> _logger;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyFollowerRepository _companyFollowerRepository;

        public UnfollowCompanyHandler(
            IUnitOfWork unitOfWork, 
            ILogger<UnfollowCompanyHandler> logger, 
            ICompanyRepository companyRepository, 
            ICompanyFollowerRepository companyFollowerRepository)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _companyRepository = companyRepository;
            _companyFollowerRepository = companyFollowerRepository;
        }

        public async Task<Unit> Handle(UnfollowCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(request.CompanyId);
            if (company == null)
                throw new NotFoundException($"Company is not found by id: {request.CompanyId}");

            var companyFollower = await _companyFollowerRepository.GetCompanyFollower(request.CompanyId, request.UserId);
            if (companyFollower == null)
                throw new NotFoundException($"Company follower is not found: CompanyId: {request.CompanyId} UserId: {request.UserId}");
            
            company.Unfollow(companyFollower);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            _logger.LogInformation("User {userId} unfollow the company {companyId}", request.UserId, request.CompanyId);
            
            return Unit.Value;
        }
    }
}