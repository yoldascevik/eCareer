using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Command;
using CurriculumVitae.Core.Entities;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.Cv.Commands.UpdateLocation;

public class UpdateLocationCommandHandler: ICommandHandler<UpdateLocationCommand>
{
    private readonly IMapper _mapper;
    private readonly ICVRepository _cvRepository;
    private readonly ILogger<UpdateLocationCommandHandler> _logger;

    public UpdateLocationCommandHandler(ICVRepository cvRepository, IMapper mapper, ILogger<UpdateLocationCommandHandler> logger)
    {
        _cvRepository = cvRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
    {
        var cv = await _cvRepository.GetByKeyAsync(request.CvId);
        if (cv == null || cv.IsDeleted)
        {
            throw new CVNotFoundException(request.CvId);
        }

        cv.Location = _mapper.Map<PersonLocation>(request.Location);

        await _cvRepository.UpdateAsync(cv.Id, cv);
        _logger.LogInformation("CV location updated: {CVId}", cv.Id);
            
        return Unit.Value;
    }
}