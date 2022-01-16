using System;
using Career.Exceptions;
using Career.MediatR.Command;

namespace Company.Application.Company.Commands.DeleteAddress;

public class DeleteAddressCommand: ICommand
{
    public DeleteAddressCommand(Guid companyId, Guid addressId)
    {
        Check.NotEmpty(companyId, nameof(companyId));
        Check.NotEmpty(addressId, nameof(addressId));
            
        CompanyId = companyId;
        AddressId = addressId;
    }
        
    public Guid CompanyId { get; }
    public Guid AddressId { get; }
}