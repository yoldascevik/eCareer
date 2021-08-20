using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Domain.Extensions;
using Career.MediatR.Command;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.DrivingLicence.Commands.Update
{
    public class UpdateDrivingLicenceCommandHandler : ICommandHandler<UpdateDrivingLicenceCommand>
    {
        private readonly IMapper _mapper;
        private readonly ICVRepository _cvRepository;
        private readonly ILogger<UpdateDrivingLicenceCommandHandler> _logger;

        public UpdateDrivingLicenceCommandHandler(ICVRepository cvRepository, IMapper mapper, ILogger<UpdateDrivingLicenceCommandHandler> logger)
        {
            _cvRepository = cvRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDrivingLicenceCommand request, CancellationToken cancellationToken)
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

            _mapper.Map(request.DrivingLicence, drivingLicence);
            await _cvRepository.UpdateAsync(cv.Id, cv);
            _logger.LogInformation("Driving licence \"{DrivingLicenceId}\" updated", request.DrivingLicenceId);

            return Unit.Value;
        }
    }
}