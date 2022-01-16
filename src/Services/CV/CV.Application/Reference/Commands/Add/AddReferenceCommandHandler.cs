using AutoMapper;
using Career.MediatR.Command;
using Career.Shared.Generators;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Application.Reference.Dtos;
using CurriculumVitae.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.Reference.Commands.Add;

public class AddReferenceCommandHandler : ICommandHandler<AddReferenceCommand, ReferenceDto>
{
    private readonly IMapper _mapper;
    private readonly ICVRepository _cvRepository;
    private readonly IStringIdGenerator _stringIdGenerator;
    private readonly ILogger<AddReferenceCommandHandler> _logger;

    public AddReferenceCommandHandler(
        ICVRepository cvRepository, 
        IMapper mapper, 
        IStringIdGenerator stringIdGenerator,
        ILogger<AddReferenceCommandHandler> logger)
    {
        _mapper = mapper;
        _logger = logger;
        _cvRepository = cvRepository;
        _stringIdGenerator = stringIdGenerator;
    }

    public async Task<ReferenceDto> Handle(AddReferenceCommand request, CancellationToken cancellationToken)
    {
        var cv = await _cvRepository.GetByKeyAsync(request.CvId);
        if (cv == null || cv.IsDeleted)
            throw new CVNotFoundException(request.CvId);
  
        var reference = _mapper.Map<Data.Entities.Reference>(request.Reference);
        reference.Id = _stringIdGenerator.Generate();

        cv.References.Add(reference);

        await _cvRepository.UpdateAsync(cv.Id, cv);
        _logger.LogInformation("New reference ({ReferenceId}) added to CV ({CvId})", reference.Id, cv.Id);

        return _mapper.Map<ReferenceDto>(reference);
    }
}