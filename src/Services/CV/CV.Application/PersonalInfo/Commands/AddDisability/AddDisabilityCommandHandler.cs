using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Command;
using Career.Shared.Generators;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Application.DisabilityType;
using CurriculumVitae.Application.PersonalInfo.Dtos;
using CurriculumVitae.Core.Refs;
using CurriculumVitae.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.PersonalInfo.Commands.AddDisability
{
    public class AddDisabilityCommandHandler : ICommandHandler<AddDisabilityCommand, DisabilityDto>
    {
        private readonly IMapper _mapper;
        private readonly ICVRepository _cvRepository;
        private readonly IStringIdGenerator _stringIdGenerator;
        private readonly ILogger<AddDisabilityCommandHandler> _logger;
        private readonly IDisabilityTypeRepository _disabilityTypeRepository;

        public AddDisabilityCommandHandler(
            ICVRepository cvRepository, 
            IDisabilityTypeRepository disabilityTypeRepository,
            IStringIdGenerator stringIdGenerator, 
            IMapper mapper, 
            ILogger<AddDisabilityCommandHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _cvRepository = cvRepository;
            _stringIdGenerator = stringIdGenerator;
            _disabilityTypeRepository = disabilityTypeRepository;
        }

        public async Task<DisabilityDto> Handle(AddDisabilityCommand request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.GetByKeyAsync(request.CvId);
            if (cv == null || cv.IsDeleted)
            {
                throw new CVNotFoundException(request.CvId);
            }

            var disabilityType = await _disabilityTypeRepository.GetByKeyAsync(request.DisabilityInfo.TypeId);
            if (disabilityType == null)
            {
                throw new DisabilityTypeNotFoundException(request.DisabilityInfo.TypeId);
            }
  
            var disability = _mapper.Map<Core.Entities.Disability>(request.DisabilityInfo);
            disability.Id = _stringIdGenerator.Generate();
            disability.Type = _mapper.Map<DisabilityTypeRef>(disabilityType);

            cv.PersonalInfo.Disabilities.Add(disability);

            await _cvRepository.UpdateAsync(cv.Id, cv);
            _logger.LogInformation("New disability ({DisabilityId}) added to CV ({CvId})", disability.Id, cv.Id);

            return _mapper.Map<DisabilityDto>(disability);
        }
    }
}