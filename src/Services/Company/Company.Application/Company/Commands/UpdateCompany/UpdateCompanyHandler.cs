using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Exceptions.Exceptions;
using Career.MediatR.Command;
using Career.Repositories;
using Company.Domain.Repository;
using Microsoft.Extensions.Logging;

namespace Company.Application.Company.Commands.UpdateCompany
{
    public class UpdateCompanyHandler : ICommandHandler<UpdateCompanyCommmand, CompanyDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<UpdateCompanyCommmand> _logger;

        public UpdateCompanyHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICompanyRepository companyRepository,
            ILogger<UpdateCompanyCommmand> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _companyRepository = companyRepository;
        }

        public async Task<CompanyDto> Handle(UpdateCompanyCommmand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeleted);
            if (company == null)
                throw new ItemNotFoundException($"Company is not found by id: {request.Id}");

            _mapper.Map(request.Company, company);
            var updatedCompany = await _companyRepository.UpdateAsync(request.Id, company);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Company is updated : {CompanyId} - {CompanyName}", request.Id, company.Name);

            return _mapper.Map<CompanyDto>(updatedCompany);
        }
    }
}