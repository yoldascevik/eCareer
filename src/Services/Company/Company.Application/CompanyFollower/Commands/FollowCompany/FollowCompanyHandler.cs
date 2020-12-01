using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Exceptions.Exceptions;
using Career.MediatR.Command;
using Career.Repositories;
using Company.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Company.Application.CompanyFollower.Commands.FollowCompany
{
    public class FollowCompanyHandler : ICommandHandler<FollowCompanyCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<FollowCompanyHandler> _logger;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyFollowerRepository _companyFollowerRepository;

        public FollowCompanyHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<FollowCompanyHandler> logger,
            ICompanyRepository companyRepository,
            ICompanyFollowerRepository companyFollowerRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _companyRepository = companyRepository;
            _companyFollowerRepository = companyFollowerRepository;
        }

        public async Task<Unit> Handle(FollowCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetCompanyIncludeFollowers(request.CompanyId);
            if (company == null)
                throw new ItemNotFoundException($"Company is not found: {request.CompanyId}");

            // TODO: specification ?
            company.Follow(request.UserId);
            
            _logger.LogInformation("User {userId} followed company {companyName} ({companyId})", request.UserId, company.Name, company.Id);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}