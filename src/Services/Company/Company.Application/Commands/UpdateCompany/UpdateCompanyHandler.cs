using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Exceptions.Exceptions;
using Career.Repositories;
using Company.Application.Dtos;
using Company.Domain.Repository;
using Company.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Company.Application.Commands.UpdateCompany
{
    public class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand, CompanyDto>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<UpdateCompanyCommand> _logger;
        private readonly IUnitOfWork<CompanyDbContext> _unitOfWork;

        public UpdateCompanyHandler(
            IMapper mapper,
            ICompanyRepository companyRepository,
            ILogger<UpdateCompanyCommand> logger,
            IUnitOfWork<CompanyDbContext> unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CompanyDto> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeleted);
            if (company == null)
                throw new ItemNotFoundException($"Company is not found by id: {request.Id}");

            _mapper.Map(request, company);
            var updatedCompany = await _companyRepository.UpdateAsync(request.Id, company);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Company is updated : {CompanyId} - {CompanyName}", request.Id, company.Name);

            return _mapper.Map<CompanyDto>(updatedCompany);
        }
    }
}