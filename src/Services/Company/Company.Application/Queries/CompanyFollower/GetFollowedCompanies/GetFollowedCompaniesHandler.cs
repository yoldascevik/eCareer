using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Career.Data.Pagination;
using Company.Application.Dtos.Company;
using Company.Domain.Repository;
using MediatR;

namespace Company.Application.Queries.CompanyFollower.GetFollowedCompanies
{
    public class GetFollowedCompaniesHandler: IRequestHandler<GetFollowedCompaniesQuery, PagedList<CompanyFollowerDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyFollowerRepository _companyFollowerRepository;

        public GetFollowedCompaniesHandler(ICompanyFollowerRepository companyFollowerRepository, IMapper mapper)
        {
            _companyFollowerRepository = companyFollowerRepository;
            _mapper = mapper;
        }

        public async Task<PagedList<CompanyFollowerDto>> Handle(GetFollowedCompaniesQuery request, CancellationToken cancellationToken)
        {
            return await _companyFollowerRepository.GetFollowedCompaniesOfUser(request.UserId)
                .ProjectTo<CompanyFollowerDto>(_mapper.ConfigurationProvider)
                .ToPagedListAsync(request.PaginationFilter);
        }
    }
}