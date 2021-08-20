using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Command;
using Career.Shared.Generators;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Application.DrivingLicence.Dtos;
using CurriculumVitae.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.DrivingLicence.Commands.Add
{
    public class AddDrivingLicenceCommandHandler : ICommandHandler<AddDrivingLicenceCommand, DrivingLicenceDto>
    {
        private readonly IMapper _mapper;
        private readonly ICVRepository _cvRepository;
        private readonly IStringIdGenerator _stringIdGenerator;
        private readonly ILogger<AddDrivingLicenceCommandHandler> _logger;

        public AddDrivingLicenceCommandHandler(
            ICVRepository cvRepository, 
            IMapper mapper, 
            IStringIdGenerator stringIdGenerator, 
            ILogger<AddDrivingLicenceCommandHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _cvRepository = cvRepository;
            _stringIdGenerator = stringIdGenerator;
        }

        public async Task<DrivingLicenceDto> Handle(AddDrivingLicenceCommand request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.GetByKeyAsync(request.CvId);
            if (cv == null || cv.IsDeleted)
            {
                throw new CVNotFoundException(request.CvId);
            }

            var drivingLicence = _mapper.Map<Core.Entities.DrivingLicence>(request.DrivingLicence);
            drivingLicence.Id = _stringIdGenerator.Generate();

            cv.DrivingLicences.Add(drivingLicence);
            await _cvRepository.UpdateAsync(cv.Id, cv);
            
            _logger.LogInformation("New driving licence {LicenceClass} added to cv {CvId}", drivingLicence.Class, cv.Id);

            return _mapper.Map<DrivingLicenceDto>(drivingLicence);
        }
    }
}