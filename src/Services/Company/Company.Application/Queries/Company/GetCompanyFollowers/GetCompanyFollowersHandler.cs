using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Career.Data.Pagination;
using Company.Application.Dtos.Company;
using Company.Domain.Repository;
using MediatR;

namespace Company.Application.Queries.Company.GetCompanyFollowers
{
    public class GetCompanyFollowersHandler : IRequestHandler<GetCompanyFollowersQuery, PagedList<CompanyFollowerDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyFollowerRepository _companyFollowerRepository;

        public GetCompanyFollowersHandler(ICompanyFollowerRepository companyFollowerRepository, IMapper mapper)
        {
            _companyFollowerRepository = companyFollowerRepository;
            _mapper = mapper;
        }

        public async Task<PagedList<CompanyFollowerDto>> Handle(GetCompanyFollowersQuery request, CancellationToken cancellationToken)
        {
            return await _companyFollowerRepository.GetActiveCompanyFollowers(request.CompanyId)
                .ProjectTo<CompanyFollowerDto>(_mapper.ConfigurationProvider)
                .ToPagedListAsync(request);
        }
    }
}