using Career.MediatR.Command;
using Company.Application.Company.Dtos;

namespace Company.Application.Company.Commands.UpdateCompanyDetails;

public class UpdateCompanyDetailsCommand : ICommand<CompanyDetailDto>
{
    public UpdateCompanyDetailsCommand(Guid companyId, CompanyDetailDto company)
    {
        CompanyId = companyId;
        Company = company;
    }
        
    public Guid CompanyId { get; }
    public CompanyDetailDto Company { get; }
}