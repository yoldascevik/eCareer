using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Data.Pagination;
using Career.Exceptions.Exceptions;
using Career.MediatR.Query;
using Company.Application.Company.Dtos;
using Company.Domain.Repositories;

namespace Company.Application.Company.Queries.GetCompanyAddresses
{
    public class GetCompanyAddressesQueryHandler: IQueryHandler<GetCompanyAddressesQuery, PagedList<AddressDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;

        public GetCompanyAddressesQueryHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<PagedList<AddressDto>> Handle(GetCompanyAddressesQuery request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(request.CompanyId);
            if (company == null)
                throw new ItemNotFoundException(request.CompanyId);
            
            var result = _mapper.Map<List<AddressDto>>(company.Addresses.Where(x=> !x.IsDeleted));

            return result.ToPagedList(request.PaginationFilter);
        }
    }
}