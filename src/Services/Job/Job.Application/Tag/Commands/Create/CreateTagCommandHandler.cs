using System;
using System.Threading;
using System.Threading.Tasks;
using Career.Exceptions.Exceptions;
using Career.MediatR.Command;
using Job.Domain.TagAggregate.Repositories;
using Microsoft.Extensions.Logging;

namespace Job.Application.Tag.Commands.Create;

public class CreateTagCommandHandler: ICommandHandler<CreateTagCommand, Guid>
{
    private readonly ITagRepository _tagRepository;
    private readonly ILogger<CreateTagCommandHandler> _logger;
        
    public CreateTagCommandHandler(ITagRepository tagRepository, ILogger<CreateTagCommandHandler> logger)
    {
        _tagRepository = tagRepository;
        _logger = logger;
    }

    public async Task<Guid> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        if (await _tagRepository.Exists(request.Name))
            throw new AlreadyExistsException($"Tag {request.Name} already exists.");
            
        var tag = Domain.TagAggregate.Tag.Create(request.Name);
        await _tagRepository.AddAsync(tag);
            
        _logger.LogInformation("Tag created: \"{TagName}\"", tag.Name);
        return tag.Id;
    }
}