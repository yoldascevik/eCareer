using System;
using Company.Application.Dtos.Company;
using MediatR;

namespace Company.Application.Commands.UpdateCompany
{
    public class UpdateCompanyCommmand: IRequest<CompanyDto>
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