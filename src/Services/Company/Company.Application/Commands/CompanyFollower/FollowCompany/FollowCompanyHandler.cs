using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Exceptions.Exceptions;
using Career.Repositories;
using Company.Domain.Repository;
using Company.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Company.Application.Commands.CompanyFollower.FollowCompany
{
    public class FollowCompanyHandler : IRequestHandler<FollowCompanyCommand>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<FollowCompanyHandler> _logger;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork<CompanyDbContext> _unitOfWork;
        private readonly ICompanyFollowerRepository _companyFollowerRepository;

        public FollowCompanyHandler(
            IMapper mapper,
            ILogger<FollowCompanyHandler> logger,
            ICompanyRepository companyRepository,
            IUnitOfWork<CompanyDbContext> unitOfWork,
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
            //TODO: check user exist
            
            var company = await _companyRepository.FirstOrDefaultAsync(c => c.Id == request.CompanyId && !c.IsDeleted);
            if (company == null)
                throw new ItemNotFoundException($"Company is not found: {request.CompanyId}");

            var companyFollower = await _companyFollowerRepository.FirstOrDefaultAsync(c => c.UserId == request.UserId && c.CompanyId == request.CompanyId);
            if (companyFollower != null && !companyFollower.IsDeleted)
            {
                throw new AlreadyExistsException($"User already follow the company.");
            }
            else if (companyFollower != null && companyFollower.IsDeleted)
            {
                companyFollower.IsDeleted = false;
                await _companyFollowerRepository.UpdateAsync(companyFollower.Id, companyFollower);
            }
            else
            {
                await _companyFollowerRepository.AddAsync(_mapper.Map<Domain.Entities.CompanyFollower>(request));
            }

            _logger.LogInformation("User {userId} followed company {companyName} ({companyId})", request.UserId, company.Name, company.Id);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}