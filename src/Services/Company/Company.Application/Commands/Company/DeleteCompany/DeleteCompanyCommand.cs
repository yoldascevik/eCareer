using System;
using Career.MediatR.Command;
using MediatR;

namespace Company.Application.Commands.Company.DeleteCompany
{
    public class DeleteCompanyCommand: ICommand
    {
        public DeleteCompanyCommand(Guid id)
        {
            Id = id;
        }
        
        public Guid Id { get; set; }
    }
}