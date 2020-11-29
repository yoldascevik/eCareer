using System;
using Career.MediatR.Command;
using Company.Application.Dtos.Company;
using MediatR;

namespace Company.Application.Commands.Company.UpdateCompany
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