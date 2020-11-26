using System;
using MediatR;

namespace Company.Application.Commands.CompanyFollower.FollowCompany
{
    public class FollowCompanyCommand: IRequest
    {
        public FollowCompanyCommand(Guid userId, Guid companyId)
        {
            UserId = userId;
            CompanyId = companyId;
        }
        
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
    }
}