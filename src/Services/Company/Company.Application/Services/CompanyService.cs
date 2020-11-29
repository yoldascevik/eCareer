using System.Threading.Tasks;
using AutoMapper;
using Company.Application.Commands.Company.CreateCompany;
using Company.Application.Dtos.Company;
using Company.Application.Services.Abstractions;
using Company.Domain.Repository;

namespace Company.Application.Services
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