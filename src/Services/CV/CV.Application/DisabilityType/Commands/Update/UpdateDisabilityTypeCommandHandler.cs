using Career.Exceptions.Exceptions;
using Career.MediatR.Command;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.DisabilityType.Commands.Update;

public class UpdateDisabilityTypeCommandHandler : ICommandHandler<UpdateDisabilityTypeCommand>
{
    private readonly IDisabilityTypeRepository _disabilityTypeRepository;
    private readonly ILogger<UpdateDisabilityTypeCommandHandler> _logger;

    public UpdateDisabilityTypeCommandHandler(
        IDisabilityTypeRepository disabilityTypeRepository,
        ILogger<UpdateDisabilityTypeCommandHandler> logger)
    {
        _disabilityTypeRepository = disabilityTypeRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateDisabilityTypeCommand request, CancellationToken cancellationToken)
    {
        var disabilityType = await _disabilityTypeRepository.GetByKeyAsync(request.Id);
        if (disabilityType == null || disabilityType.IsDeleted)
            throw new DisabilityTypeNotFoundException(request.Id);

        var existingTypeForName = await _disabilityTypeRepository.GetByNameAsync(request.Name);
        if (existingTypeForName != null && existingTypeForName.Id != disabilityType.Id)
            throw new ItemAlreadyExistsException(request.Name);

        disabilityType.Name = request.Name;
        await _disabilityTypeRepository.UpdateAsync(disabilityType.Id, disabilityType);

        _logger.LogInformation("Disability type updated {Name}", disabilityType.Name);
        return Unit.Value;
    }
}