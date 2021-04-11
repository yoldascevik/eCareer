using System;
using Career.MediatR.Command;
using Job.Application.Tag.Dtos;

namespace Job.Application.Tag.Commands.Update
{
    public class UpdateTagCommand: ICommand<TagDto>
    {
        public UpdateTagCommand(Guid tagId, string name)
        {
            TagId = tagId;
            Name = name;
        }
        
        public Guid TagId { get; }
        public string Name { get; }
    }
}