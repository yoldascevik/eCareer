using System;
using Career.MediatR.Command;

namespace Company.Application.Company.Commands.UpdateCompany
{
    public class UpdateCompanyCommmand: ICommand<CompanyDto>
    {
        public UpdateCompanyCommmand(Guid id, CompanyRequest company)
        {
            Id = id;
            Company = company;
        }
        
        public Guid Id { get; set; }
        public CompanyRequest Company { get; set; }
    }
}