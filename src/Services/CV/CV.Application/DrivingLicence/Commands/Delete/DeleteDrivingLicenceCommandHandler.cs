using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Career.Domain.Extensions;
using Career.MediatR.Command;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.DrivingLicence.Commands.Delete
{
    public class DeleteDrivingLicenceCommandHandler : ICommandHandler<DeleteDrivingLicenceCommand>
    {
        private readonly ICVRepository _cvRepository;
        private readonly ILogger<DeleteDrivingLicenceCommandHandler> _logger;

        public DeleteDrivingLicenceCommandHandler(ICVRepository cvRepository, ILogger<DeleteDrivingLicenceCommandHandler> logger)
        {
            _cvRepository = cvRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteDrivingLicenceCommand request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.GetByKeyAsync(request.CvId);
            if (cv == null || cv.IsDeleted)
            {
                throw new CVNotFoundException(request.CvId);
            }

            var drivingLicence = cv.DrivingLicences.ExcludeDeletedItems().FirstOrDefault(x => x.Id == request.DrivingLicenceId);
            if (drivingLicence == null)
            {
                throw new DrivingLicenceNotFoundException(request.DrivingLicenceId);
            }

            drivingLicence.IsDeleted = true;
            await _cvRepository.UpdateAsync(cv.Id, cv);
            
            _logger.LogInformation("Driving licence \"{DrivingLicenceId}\" is deleted", drivingLicence.Id);
            
            return Unit.Value;
        }
    }
}