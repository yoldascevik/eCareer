using Career.MediatR.Command;

namespace Company.Application.Company.Commands.CreateCompany
{
    public class CreateCompanyCommand : CompanyRequest, ICommand<CompanyDto>
    {
    }
}