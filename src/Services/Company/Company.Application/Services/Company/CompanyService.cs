using System.Threading.Tasks;
using AutoMapper;
using Company.Application.Company;
using Company.Application.Company.Commands.CreateCompany;
using Company.Domain.Repository;

namespace Company.Application.Services.Company
{
    internal class CompanyService: ICompanyService
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;
        
        public CompanyService(IMapper mapper, ICompanyRepository companyRepository)
        {
            _mapper = mapper;
            _companyRepository = companyRepository;
        }

        public async Task<CompanyDto> CreateCompanyAsync(CreateCompanyCommand command)
        {
            var company = _mapper.Map<Domain.Entities.Company>(command);
            var createdCompany = await _companyRepository.AddAsync(company);
            
            return _mapper.Map<CompanyDto>(createdCompany);
        }
    }
}