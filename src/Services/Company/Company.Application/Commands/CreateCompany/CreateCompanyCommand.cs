using Company.Application.Dtos.Company;
using MediatR;

namespace Company.Application.Commands.CreateCompany
{
    public class CreateCompanyCommand: CompanyRequest, IRequest<CompanyDto>
    {
       
    }
}