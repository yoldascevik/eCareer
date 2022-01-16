using System;
using Career.Exceptions;
using Career.MediatR.Query;
using Company.Application.Company.Dtos;

namespace Company.Application.Company.Queries.GetCompanyAddressById;

public class GetCompanyAddressesByIdQuery: IQuery<AddressDto>
{
    public GetCompanyAddressesByIdQuery(Guid companyId, Guid addressId)
    {
        Check.NotEmpty(companyId, nameof(companyId));
        Check.NotEmpty(addressId, nameof(addressId));
           
        CompanyId = companyId;
        AddressId = addressId;
    }

    public Guid CompanyId { get; }
    public Guid AddressId { get; }
}