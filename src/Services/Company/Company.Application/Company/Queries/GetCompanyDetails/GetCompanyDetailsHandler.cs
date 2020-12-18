using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Exceptions.Exceptions;
using Career.MediatR.Query;
using Company.Application.Company.Dtos;
using Company.Domain.Repositories;

namespace Company.Application.Company.Queries.GetCompanyDetails
{
    public class GetCompanyAddressHandler: IQueryHandler<GetCompanyDetailsQuery, CompanyDetailDto>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;

        public GetCompanyAddressHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<CompanyDetailDto> Handle(GetCompanyDetailsQuery request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(request.CompanyId);
            if (company == null)
                throw new ItemNotFoundException(request.CompanyId);
            
            return _mapper.Map<CompanyDetailDto>(company);
        }
    }
}