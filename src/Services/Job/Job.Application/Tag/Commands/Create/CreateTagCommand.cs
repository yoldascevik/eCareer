using System;
using Career.MediatR.Command;

namespace Job.Application.Tag.Commands.Create;

public class CreateTagCommand: ICommand<Guid>
{
    public string Name { get; set; }
}