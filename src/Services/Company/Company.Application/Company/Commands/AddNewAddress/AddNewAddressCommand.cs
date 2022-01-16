using System;
using Career.Exceptions;
using Career.MediatR.Command;
using Company.Application.Company.Dtos;

namespace Company.Application.Company.Commands.AddNewAddress;

public class AddNewAddressCommand: ICommand<Guid>
{
    public AddNewAddressCommand(Guid companyId, AddressInputDto addressDto)
    {
        Check.NotEmpty(companyId, nameof(companyId));
        Check.NotNull(addressDto,nameof(addressDto));
            
        CompanyId = companyId;
        AddressDto = addressDto;
    }
        
    public Guid CompanyId { get; }
    public AddressInputDto AddressDto { get; }
}