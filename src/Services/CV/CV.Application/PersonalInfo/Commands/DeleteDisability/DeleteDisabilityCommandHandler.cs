using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Career.Domain.Extensions;
using Career.MediatR.Command;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Application.PersonalInfo.Exceptions;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.PersonalInfo.Commands.DeleteDisability;

public class DeleteDisabilityCommandHandler : ICommandHandler<DeleteDisabilityCommand>
{
    private readonly ICVRepository _cvRepository;
    private readonly ILogger<DeleteDisabilityCommandHandler> _logger;

    public DeleteDisabilityCommandHandler(ICVRepository cvRepository, ILogger<DeleteDisabilityCommandHandler> logger)
    {
        _cvRepository = cvRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteDisabilityCommand request, CancellationToken cancellationToken)
    {
        var cv = await _cvRepository.GetByKeyAsync(request.CvId);
        if (cv == null || cv.IsDeleted)
        {
            throw new CVNotFoundException(request.CvId);
        }
            
        var disability = cv.PersonalInfo.Disabilities.ExcludeDeletedItems().FirstOrDefault(x=> x.Id == request.Id);
        if (disability == null)
        {
            throw new DisabilityNotFoundException(request.Id);
        }

        disability.IsDeleted = true;
        await _cvRepository.UpdateAsync(cv.Id, cv);

        _logger.LogInformation("Disability ({DisabilityId}) deleted in CV ({CvId})", disability.Id, cv.Id);

        return Unit.Value;
    }
}