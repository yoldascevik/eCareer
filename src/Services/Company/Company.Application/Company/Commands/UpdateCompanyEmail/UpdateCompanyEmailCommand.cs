using System;
using Career.MediatR.Command;

namespace Company.Application.Company.Commands.UpdateCompanyEmail;

public class UpdateCompanyEmailCommand : ICommand
{
    public UpdateCompanyEmailCommand(Guid companyId, string email)
    {
        CompanyId = companyId;
        Email = email;
    }

    public Guid CompanyId { get; }
    public string Email { get; }
}