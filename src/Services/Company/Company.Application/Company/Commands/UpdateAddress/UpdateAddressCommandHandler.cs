using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Exceptions;
using Career.MediatR.Command;
using Company.Application.Company.Dtos;
using Company.Application.Company.Exceptions;
using Company.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Company.Application.Company.Commands.UpdateAddress
{
    public class UpdateAddressCommandHandler: ICommandHandler<UpdateAddressCommand, AddressDto>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<UpdateAddressCommandHandler> _logger;

        public UpdateAddressCommandHandler(ICompanyRepository companyRepository, IMapper mapper, ILogger<UpdateAddressCommandHandler> logger)
        {
            Check.NotNull(companyRepository, nameof(companyRepository));
            Check.NotNull(mapper, nameof(mapper));
            Check.NotNull(logger, nameof(logger));
            
            _companyRepository = companyRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AddressDto> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(request.CompanyId);
            if (company == null)
                throw new CompanyNotFoundException(request.CompanyId);

            var address = company.Addresses.FirstOrDefault(x => x.Id == request.AddressId);
            if (address == null)
                throw new AddressNotFoundException(request.AddressId);

            if (!address.IsPrimary && request.AddressDto.IsPrimary)
            {
                var primaryAddress = company.Addresses.SingleOrDefault(x => x.IsPrimary);
                if (primaryAddress != null)
                {
                    primaryAddress.SetPrimary(false);
                }
            }
            
            address.SetTitle(request.AddressDto.Title)
                .SetDetails(request.AddressDto.Details)
                .SetCountry(request.AddressDto.Country)
                .SetCity(request.AddressDto.City)
                .SetDistrict(request.AddressDto.District)
                .SetZipCode(request.AddressDto.ZipCode)
                .SetPrimary(request.AddressDto.IsPrimary);
            
            await _companyRepository.UpdateAsync(company.Id, company);
            
            _logger.LogInformation("Address {AddressId} updated for company: {CompanyId}", address.Id, company.Id);
            return _mapper.Map<AddressDto>(address);
        }
    }
}