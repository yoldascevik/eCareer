using System;
using Career.MediatR.Command;

namespace Company.Application.CompanyFollower.Commands.UnfollowCompany;

public class UnfollowCompanyCommand: ICommand
{
    public UnfollowCompanyCommand(Guid userId, Guid companyId)
    {
        UserId = userId;
        CompanyId = companyId;
    }

    public Guid UserId { get; set; }
    public Guid CompanyId { get; set; }
}