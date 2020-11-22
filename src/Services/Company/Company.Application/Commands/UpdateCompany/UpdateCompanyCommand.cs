using System;
using Company.Application.Commands.CreateCompany;

namespace Company.Application.Commands.UpdateCompany
{
    public class UpdateCompanyCommand: CreateCompanyCommand
    { 
        internal Guid Id { get; set; }
    }
}