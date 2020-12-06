using System.Threading;
using System.Threading.Tasks;
using Career.Exceptions.Exceptions;
using Career.MediatR.Command;
using Career.Repositories;
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

        public UnfollowCompanyHandler(
            IUnitOfWork unitOfWork, 
            ILogger<UnfollowCompanyHandler> logger, 
            ICompanyRepository companyRepository)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _companyRepository = companyRepository;
        }

        public async Task<Unit> Handle(UnfollowCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(request.CompanyId);
            if (company == null)
                throw new ItemNotFoundException($"Company is not found: {request.CompanyId}");

            // company.Unfollow(request.UserId); //TODO
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            _logger.LogInformation("User {userId} unfollow the company {companyId}", request.UserId, request.CompanyId);
            
            return Unit.Value;
        }
    }
}