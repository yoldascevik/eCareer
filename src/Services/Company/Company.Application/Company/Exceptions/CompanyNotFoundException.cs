using System;
using Career.Exceptions.Exceptions;

namespace Company.Application.Company.Exceptions
{
    public class CompanyNotFoundException: NotFoundException
    {
        public CompanyNotFoundException(Guid companyId) : base($"Company is not found by id: {companyId}")
        {
        }
    }
}