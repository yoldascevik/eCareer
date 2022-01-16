using AutoMapper;
using AutoMapper.QueryableExtensions;
using Career.Data.Pagination;
using Career.MediatR.Query;
using Company.Application.Company.Dtos;
using Company.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Company.Application.Company.Queries.GetCompanies;

public class GetCompaniesQueryHandler : IQueryHandler<GetCompaniesQuery, PagedList<CompanyDto>>
{
    private readonly IMapper _mapper;
    private readonly ICompanyRepository _companyRepository;

    public GetCompaniesQueryHandler(ICompanyRepository companyRepository, IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<PagedList<CompanyDto>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
    {
        return await _companyRepository.Get(company => !company.IsDeleted)
            .Include(p=> p.Sector)
            .AsNoTracking()
            .ProjectTo<CompanyDto>(_mapper.ConfigurationProvider)
            .ToPagedListAsync(request);
    }
}