using Company.Application.Dtos;
using MediatR;

namespace Company.Application.Commands.CreateCompany
{
    public class CreateCompanyCommand: CompanyCommandModel, IRequest<CompanyDto>
    {
       
    }
}