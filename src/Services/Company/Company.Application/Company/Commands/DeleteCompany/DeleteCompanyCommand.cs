using System;
using Career.MediatR.Command;

namespace Company.Application.Company.Commands.DeleteCompany
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