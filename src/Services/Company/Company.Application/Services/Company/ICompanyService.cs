using System.Threading.Tasks;
using Company.Application.Company;
using Company.Application.Company.Commands.CreateCompany;

namespace Company.Application.Services.Company
{
    public interface ICompanyService
    {
        Task<CompanyDto> CreateCompanyAsync(CreateCompanyCommand command);
    }
}