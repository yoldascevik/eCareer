using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Company.Domain.Repository;
using MediatR;

namespace Company.Application.Commands.CreateCompany
{
    public class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, CompanyDto>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;

        public CreateCompanyHandler(
            IMapper mapper,
            ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var createdCompany = _mapper.Map<Domain.Company>(request);
            return _mapper.Map<CompanyDto>(await _companyRepository.AddAsync(createdCompany));
        }
    }
}