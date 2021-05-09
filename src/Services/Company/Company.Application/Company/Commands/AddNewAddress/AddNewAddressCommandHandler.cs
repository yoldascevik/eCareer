using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Exceptions;
using Career.MediatR.Command;
using Company.Application.Company.Exceptions;
using Company.Domain.Entities;
using Company.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Company.Application.Company.Commands.AddNewAddress
{
    public class AddNewAddressCommandHandler: ICommandHandler<AddNewAddressCommand, Guid>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<AddNewAddressCommandHandler> _logger;

        public AddNewAddressCommandHandler(ICompanyRepository companyRepository, IMapper mapper, ILogger<AddNewAddressCommandHandler> logger)
        {
            Check.NotNull(companyRepository, nameof(companyRepository));
            Check.NotNull(mapper, nameof(mapper));
            Check.NotNull(logger, nameof(logger));
            
            _companyRepository = companyRepository;
            _logger = logger;
        }

        public async Task<Guid> Handle(AddNewAddressCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(request.CompanyId);
            if (company == null)
                throw new CompanyNotFoundException(request.CompanyId);

            var address = Address.Create(
                company.Id,
                request.AddressDto.Title,
                request.AddressDto.Country,
                request.AddressDto.City,
                request.AddressDto.District,
                request.AddressDto.Details,
                request.AddressDto.ZipCode,
                request.AddressDto.IsPrimary);

            if (address.IsPrimary)
            {
                var primaryAddress = company.Addresses.SingleOrDefault(x => x.IsPrimary);
                if (primaryAddress != null)
                {
                    primaryAddress.SetPrimary(false);
                }
            }
            
            company.AddAddress(address);
            await _companyRepository.UpdateAsync(company.Id, company);
            
            _logger.LogInformation("New address added to company: {CompanyId}", company.Id);

            return address.Id;
        }
    }
}