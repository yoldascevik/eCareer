using System;
using Career.MediatR.Command;
using Company.Application.Company.Dtos;

namespace Company.Application.Company.Commands.UpdateCompanyAddress
{
    public class UpdateCompanyAddressCommand : ICommand<AddressDto>
    {
        public UpdateCompanyAddressCommand(Guid companyId, AddressDto address)
        {
            CompanyId = companyId;
            Address = address;
        }

        public Guid CompanyId { get; }
        public AddressDto Address { get; }
    }
}