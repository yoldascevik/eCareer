using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Exceptions.Exceptions;
using Career.MediatR.Command;
using Career.Repositories.UnitOfWok;
using Company.Application.Company.Dtos;
using Company.Domain.Repositories;
using Company.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Company.Application.Company.Commands.UpdateCompanyAddress
{
    public class UpdateCompanyAddressHandler : ICommandHandler<UpdateCompanyAddressCommand, AddressDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<UpdateCompanyAddressHandler> _logger;

        public UpdateCompanyAddressHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICompanyRepository companyRepository,
            ILogger<UpdateCompanyAddressHandler> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _companyRepository = companyRepository;
        }

        public async Task<AddressDto> Handle(UpdateCompanyAddressCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(request.CompanyId);
            if (company == null)
                throw new NotFoundException($"Company is not found by id: {request.CompanyId}");
            
            var address = AddressInfo.Create(request.Address.CountryId, request.Address.CityId, request.Address.DistrictId, request.Address.Address);
            
            company.UpdateAddress(address);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            _logger.LogInformation("Company address info updated : {CompanyId}", request.CompanyId);
            
            return _mapper.Map<AddressDto>(company.AddressInfo);
        }
    }
}