using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Exceptions.Exceptions;
using Career.MediatR.Query;
using Company.Application.Company.Dtos;
using Company.Application.Company.Exceptions;
using Company.Domain.Repositories;

namespace Company.Application.Company.Queries.GetCompanyAddressById;

public class GetCompanyAddressByIdQueryHandler: IQueryHandler<GetCompanyAddressesByIdQuery, AddressDto>
{
    private readonly IMapper _mapper;
    private readonly ICompanyRepository _companyRepository;

    public GetCompanyAddressByIdQueryHandler(ICompanyRepository companyRepository, IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<AddressDto> Handle(GetCompanyAddressesByIdQuery request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetCompanyByIdAsync(request.CompanyId);
        if (company == null)
            throw new ItemNotFoundException(request.CompanyId);

        var address = company.Addresses.FirstOrDefault(x => x.Id == request.AddressId);
        if (address == null)
            throw new AddressNotFoundException(request.AddressId);
            
        return _mapper.Map<AddressDto>(address);
    }
}