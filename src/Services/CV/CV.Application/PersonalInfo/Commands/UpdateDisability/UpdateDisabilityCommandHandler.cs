using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Domain.Extensions;
using Career.MediatR.Command;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Application.DisabilityType;
using CurriculumVitae.Application.PersonalInfo.Exceptions;
using CurriculumVitae.Core.Refs;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.PersonalInfo.Commands.UpdateDisability;

public class UpdateDisabilityCommandHandler:ICommandHandler<UpdateDisabilityCommand>
{
    private readonly IMapper _mapper;
    private readonly ICVRepository _cvRepository;
    private readonly ILogger<UpdateDisabilityCommandHandler> _logger;
    private readonly IDisabilityTypeRepository _disabilityTypeRepository;

    public UpdateDisabilityCommandHandler(
        ICVRepository cvRepository, 
        IDisabilityTypeRepository disabilityTypeRepository,
        IMapper mapper, 
        ILogger<UpdateDisabilityCommandHandler> logger)
    {
        _mapper = mapper;
        _logger = logger;
        _cvRepository = cvRepository;
        _disabilityTypeRepository = disabilityTypeRepository;
    }

    public async Task<Unit> Handle(UpdateDisabilityCommand request, CancellationToken cancellationToken)
    {
        var cv = await _cvRepository.GetByKeyAsync(request.CvId);
        if (cv == null || cv.IsDeleted)
        {
            throw new CVNotFoundException(request.CvId);
        }
            
        var disability = cv.PersonalInfo.Disabilities.ExcludeDeletedItems().FirstOrDefault(x=> x.Id == request.DisabilityId);
        if (disability == null)
        {
            throw new DisabilityNotFoundException(request.DisabilityId);
        }

        var disabilityType = await _disabilityTypeRepository.GetByKeyAsync(request.DisabilityInfo.TypeId);
        if (disabilityType == null)
        {
            throw new DisabilityTypeNotFoundException(request.DisabilityInfo.TypeId);
        }
            
        _mapper.Map(request.DisabilityInfo, disability);

        if (disabilityType.Id != disability.Type.Id)
        {
            disability.Type = _mapper.Map<DisabilityTypeRef>(disabilityType);
        }

        await _cvRepository.UpdateAsync(cv.Id, cv);

        _logger.LogInformation("Disability ({DisabilityId}) updated in CV ({CvId})", disability.Id, cv.Id);

        return Unit.Value;
    }
}