using System;
using Career.MediatR.Command;

namespace Company.Application.CompanyFollower.Commands.FollowCompany;

public class FollowCompanyCommand: ICommand
{
    public FollowCompanyCommand(Guid userId, Guid companyId)
    {
        UserId = userId;
        CompanyId = companyId;
    }
        
    public Guid UserId { get; set; }
    public Guid CompanyId { get; set; }
}