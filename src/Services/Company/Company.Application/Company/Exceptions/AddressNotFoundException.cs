using Career.Exceptions.Exceptions;

namespace Company.Application.Company.Exceptions;

public class AddressNotFoundException: NotFoundException
{
    public AddressNotFoundException(Guid addressId) 
        : base($"Address is not found by id: {addressId}")
    {
    }
}