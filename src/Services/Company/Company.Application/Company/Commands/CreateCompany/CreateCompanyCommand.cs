using Career.MediatR.Command;
using Company.Application.Company.Models;

namespace Company.Application.Company.Commands.CreateCompany
{
    public class CreateCompanyCommand : CompanyRequest, ICommand<CompanyDto>
    {
    }
}