using System;
using Company.Application.Dtos.Company;
using MediatR;

namespace Company.Application.Commands.UpdateCompany
{
    public class UpdateCompanyCommand: CompanyCommandModel, IRequest<CompanyDto>
    {
        public Guid Id { get; set; }
    }
}