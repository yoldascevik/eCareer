using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Career.Utilities.Pagination;
using Company.Application.Dtos;
using Company.Domain.Repository;
using MediatR;

namespace Company.Application.Company.GetCompanies
{
    public class GetCompaniesHandler : IRequestHandler<GetCompaniesQuery, PagedList<CompanyDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;

        public GetCompaniesHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<PagedList<CompanyDto>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
        {
            //TODO: relation id mapping ?
            return await _companyRepository.Get(company => !company.IsDeleted)
                .ProjectTo<CompanyDto>(_mapper.ConfigurationProvider)
                .ToPagedListAsync(request);
        }
    }
}