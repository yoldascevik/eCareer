using System.Threading.Tasks;
using Company.Application.Company.Commands.CreateCompany;
using Company.Application.Company.Models;

namespace Company.Application.Company.Services
{
    public interface ICompanyService
    {
        Task<CompanyDto> CreateCompanyAsync(CreateCompanyCommand command);
    }
}