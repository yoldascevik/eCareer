using System.Threading.Tasks;
using Company.Application.Commands.Company.CreateCompany;
using Company.Application.Dtos.Company;

namespace Company.Application.Services.Abstractions
{
    public interface ICompanyService
    {
        Task<CompanyDto> CreateCompanyAsync(CreateCompanyCommand command);
    }
}