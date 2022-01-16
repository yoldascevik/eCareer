using Career.MediatR.Command;

namespace Company.Application.Company.Commands.DeleteCompany;

public class DeleteCompanyCommand : ICommand
{
    public DeleteCompanyCommand(Guid companyId)
    {
        CompanyId = companyId;
    }

    public Guid CompanyId { get; set; }
}