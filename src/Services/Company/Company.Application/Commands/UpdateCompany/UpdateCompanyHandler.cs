using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Exceptions.Exceptions;
using Company.Application.Dtos;
using Company.Domain.Repository;
using MediatR;

namespace Company.Application.Commands.UpdateCompany
{
    public class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand, CompanyDto>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;

        public UpdateCompanyHandler(IMapper mapper, ICompanyRepository companyRepository)
        {
            _mapper = mapper;
            _companyRepository = companyRepository;
        }

        public async Task<CompanyDto> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByKeyAsync(request.Id);
            if (company == null)
                throw new ItemNotFoundException($"Company is not found by id: {request.Id}");

            _mapper.Map(request, company);
            var updatedCompany = await _companyRepository.UpdateAsync(request.Id, company);

            return _mapper.Map<CompanyDto>(updatedCompany);
        }
    }
}