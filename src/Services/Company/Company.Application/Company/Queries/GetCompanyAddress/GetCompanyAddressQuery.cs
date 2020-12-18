using System;
using Career.MediatR.Query;
using Company.Application.Company.Dtos;

namespace Company.Application.Company.Queries.GetCompanyAddress
{
    public class GetCompanyAddressQuery: IQuery<AddressDto>
    {
        public GetCompanyAddressQuery(Guid companyId)
        {
            CompanyId = companyId;
        }

        public Guid CompanyId { get; }
    }
}