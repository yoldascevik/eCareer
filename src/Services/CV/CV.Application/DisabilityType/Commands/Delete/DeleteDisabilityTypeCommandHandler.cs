using Career.MediatR.Command;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.DisabilityType.Commands.Delete;

public class DeleteDisabilityTypeCommandHandler : ICommandHandler<DeleteDisabilityTypeCommand>
{
    private readonly IDisabilityTypeRepository _disabilityTypeRepository;
    private readonly ILogger<DeleteDisabilityTypeCommandHandler> _logger;

    public DeleteDisabilityTypeCommandHandler(
        IDisabilityTypeRepository disabilityTypeRepository, 
        ILogger<DeleteDisabilityTypeCommandHandler> logger)
    {
        _disabilityTypeRepository = disabilityTypeRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteDisabilityTypeCommand request, CancellationToken cancellationToken)
    {
        var disabilityType = await _disabilityTypeRepository.GetByKeyAsync(request.Id);
        if (disabilityType == null || disabilityType.IsDeleted)
            throw new DisabilityTypeNotFoundException(request.Id);

        disabilityType.IsDeleted = true;
        await _disabilityTypeRepository.UpdateAsync(disabilityType.Id, disabilityType);

        _logger.LogInformation("Disability type deleted {Name}", disabilityType.Name);
        return Unit.Value;
    }
}