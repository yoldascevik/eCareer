using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Exceptions.Exceptions;
using Career.MediatR.Query;
using Company.Application.Company.Dtos;
using Company.Domain.Repositories;

namespace Company.Application.Company.Queries.GetCompanyTaxInfo
{
    public class GetCompanyTaxInfoHandler: IQueryHandler<GetCompanyTaxInfoQuery, TaxDto>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;

        public GetCompanyTaxInfoHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<TaxDto> Handle(GetCompanyTaxInfoQuery request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(request.CompanyId);
            if (company == null)
                throw new ItemNotFoundException(request.CompanyId);
            
            return _mapper.Map<TaxDto>(company.TaxInfo);
        }
    }
}