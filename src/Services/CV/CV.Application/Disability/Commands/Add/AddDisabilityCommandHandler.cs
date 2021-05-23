using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Command;
using CurriculumVitae.Application.Cv.Exceptions;
using CurriculumVitae.Application.Disability.Dtos;
using CurriculumVitae.Core.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace CurriculumVitae.Application.Disability.Commands.Add
{
    public class AddDisabilityCommandHandler : ICommandHandler<AddDisabilityCommand, DisabilityDto>
    {
        private readonly IMapper _mapper;
        private readonly ICVRepository _cvRepository;
        private readonly ILogger<AddDisabilityCommandHandler> _logger;

        public AddDisabilityCommandHandler(ICVRepository cvRepository, IMapper mapper, ILogger<AddDisabilityCommandHandler> logger)
        {
            _cvRepository = cvRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DisabilityDto> Handle(AddDisabilityCommand request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.GetByKeyAsync(request.CvId);
            if (cv == null || cv.IsDeleted)
            {
                throw new CVNotFoundException(request.CvId);
            }
            
            var disability = _mapper.Map<Core.Entities.Disability>(request.DisabilityInfo);
            cv.PersonalInfo.Disabilities.Add(disability);

            await _cvRepository.UpdateAsync(cv.Id, cv);
            _logger.LogInformation("New disability ({DisabilityId}) added to CV ({CvId})", disability.Id, cv.Id);

            return _mapper.Map<DisabilityDto>(disability);
        }
    }
}