using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Career.Exceptions.Exceptions;
using Career.Repositories;
using Company.Domain.Repository;
using Company.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Company.Application.Commands.CompanyFollower.UnfollowCompany
{
    public class UnfollowCompanyHandler: IRequestHandler<UnfollowCompanyCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UnfollowCompanyHandler> _logger;
        private readonly ICompanyFollowerRepository _companyFollowerRepository;

        public UnfollowCompanyHandler(
            IUnitOfWork unitOfWork, 
            ILogger<UnfollowCompanyHandler> logger, 
            ICompanyFollowerRepository companyFollowerRepository)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _companyFollowerRepository = companyFollowerRepository;
        }

        public async Task<Unit> Handle(UnfollowCompanyCommand request, CancellationToken cancellationToken)
        {
            var companyFollower = _companyFollowerRepository.GetActiveCompanyFollowers(request.CompanyId)
                .FirstOrDefault(follower => follower.UserId == request.UserId);

            if (companyFollower == null)
                throw new ItemNotFoundException("Company follower is not found");

            await _companyFollowerRepository.DeleteAsync(companyFollower.Id);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            _logger.LogInformation("User {userId} unfollow the company {companyId}", request.UserId, request.CompanyId);
            
            return Unit.Value;
        }
    }
}