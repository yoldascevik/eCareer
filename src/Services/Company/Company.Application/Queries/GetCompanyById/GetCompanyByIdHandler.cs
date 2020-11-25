using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Exceptions.Exceptions;
using Company.Application.Dtos;
using Company.Domain.Repository;
using MediatR;

namespace Company.Application.Queries.GetCompanyById
{
    public class GetCompanyByIdHandler: IRequestHandler<GetCompanyByIdQuery, CompanyDto>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;

        public GetCompanyByIdHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<CompanyDto> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.FirstOrDefaultAsync(c => c.Id == request.CompanyId && !c.IsDeleted);
            if (company == null)
                throw new ItemNotFoundException(request.CompanyId);
            
            return _mapper.Map<CompanyDto>(company);
        }
    }
}