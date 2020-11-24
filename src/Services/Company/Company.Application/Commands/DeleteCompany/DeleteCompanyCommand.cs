using System;
using MediatR;

namespace Company.Application.Commands.DeleteCompany
{
    public class DeleteCompanyCommand: IRequest
    {
        public DeleteCompanyCommand(Guid id)
        {
            Id = id;
        }
        
        public Guid Id { get; set; }
    }
}