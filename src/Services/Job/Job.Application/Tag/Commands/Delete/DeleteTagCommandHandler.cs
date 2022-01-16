using System.Threading;
using System.Threading.Tasks;
using Career.MediatR.Command;
using Job.Application.Tag.Exceptions;
using Job.Domain.TagAggregate.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Job.Application.Tag.Commands.Delete;

public class DeleteTagCommandHandler: ICommandHandler<DeleteTagCommand>
{
    private readonly ITagRepository _tagRepository;
    private readonly ILogger<DeleteTagCommandHandler> _logger;

    public DeleteTagCommandHandler(ITagRepository tagRepository, ILogger<DeleteTagCommandHandler> logger)
    {
        _tagRepository = tagRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await _tagRepository.GetByIdAsync(request.TagId);
        if (tag is null)
            throw new TagNotFoundException(request.TagId);

        tag.MarkAsDelete();
        await _tagRepository.UpdateAsync(tag.Id, tag);

        _logger.LogInformation("Tag is deleted : {TagId}", tag.Id);

        return Unit.Value;
    }
}