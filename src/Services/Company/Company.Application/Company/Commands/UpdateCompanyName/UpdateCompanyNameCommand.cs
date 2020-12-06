using System;
using Career.MediatR.Command;

namespace Company.Application.Company.Commands.UpdateCompanyName
{
    public class UpdateCompanyNameCommand : ICommand
    {
        public UpdateCompanyNameCommand(Guid companyId, string companyName)
        {
            CompanyId = companyId;
            CompanyName = companyName;
        }

        public Guid CompanyId { get; }
        public string CompanyName { get;}
    }
}