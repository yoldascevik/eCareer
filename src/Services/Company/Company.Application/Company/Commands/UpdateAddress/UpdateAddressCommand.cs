using System;
using Career.Exceptions;
using Career.MediatR.Command;
using Company.Application.Company.Dtos;

namespace Company.Application.Company.Commands.UpdateAddress;

public class UpdateAddressCommand: ICommand<AddressDto>
{
    public UpdateAddressCommand(Guid companyId, Guid addressId, AddressInputDto addressDto)
    {
        Check.NotEmpty(companyId, nameof(companyId));
        Check.NotEmpty(addressId, nameof(addressId));
        Check.NotNull(addressDto,nameof(addressDto));
            
        CompanyId = companyId;
        AddressId = addressId;
        AddressDto = addressDto;
    }
        
    public Guid CompanyId { get; }
    public Guid AddressId { get; }
    public AddressInputDto AddressDto { get; }
}