using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Command;
using CurriculumVitae.Application.Cv.Exceptions;
using CurriculumVitae.Application.Disability.Exceptions;
using CurriculumVitae.Application.DisabilityType.Exceptions;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.Disability.Commands.Update
{
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
            
            var disability = cv.PersonalInfo.Disabilities.FirstOrDefault(x=> x.Id == request.DisabilityId && !x.IsDeleted);
            if (disability == null)
            {
                throw new DisabilityNotFoundException(request.DisabilityId);
            }

            if (!await _disabilityTypeRepository.ExistsByIdAsync(request.DisabilityInfo.TypeId))
            {
                throw new DisabilityTypeNotFoundException(request.DisabilityInfo.TypeId);
            }
            
            _mapper.Map(request.DisabilityInfo, disability);
            await _cvRepository.UpdateAsync(cv.Id, cv);

            _logger.LogInformation("Disability ({DisabilityId}) updated in CV ({CvId})", disability.Id, cv.Id);

            return Unit.Value;
        }
    }
}