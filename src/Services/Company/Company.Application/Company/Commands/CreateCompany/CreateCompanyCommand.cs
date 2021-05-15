using System;
using Career.MediatR.Command;
using Company.Application.Company.Dtos;

namespace Company.Application.Company.Commands.CreateCompany
{
    public class CreateCompanyCommand : ICommand<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public TaxDto TaxInfo { get; set; }
        public IdNameRefDto Sector { get; set; }
    }
}