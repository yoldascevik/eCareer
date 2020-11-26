using Company.Application.Dtos.Company;
using MediatR;

namespace Company.Application.Commands.Company.CreateCompany
{
    public class CreateCompanyCommand: CompanyRequest, IRequest<CompanyDto>
    {
       
    }
}