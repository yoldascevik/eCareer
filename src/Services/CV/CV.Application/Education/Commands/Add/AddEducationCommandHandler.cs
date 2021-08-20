using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Command;
using Career.Shared.Generators;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Application.Education.Dtos;
using CurriculumVitae.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.Education.Commands.Add
{
    public class AddEducationCommandHandler : ICommandHandler<AddEducationCommand, EducationDto>
    {
        private readonly IMapper _mapper;
        private readonly ICVRepository _cvRepository;
        private readonly IStringIdGenerator _stringIdGenerator;
        private readonly ILogger<AddEducationCommandHandler> _logger;

        public AddEducationCommandHandler(
            ICVRepository cvRepository, 
            IMapper mapper, 
            IStringIdGenerator stringIdGenerator,
            ILogger<AddEducationCommandHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _cvRepository = cvRepository;
            _stringIdGenerator = stringIdGenerator;
        }

        public async Task<EducationDto> Handle(AddEducationCommand request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.GetByKeyAsync(request.CvId);
            if (cv == null || cv.IsDeleted)
                throw new CVNotFoundException(request.CvId);
  
            var education = _mapper.Map<Core.Entities.Education>(request.Education);
            education.Id = _stringIdGenerator.Generate();

            cv.Educations.Add(education);

            await _cvRepository.UpdateAsync(cv.Id, cv);
            _logger.LogInformation("New education ({EducationId}) added to CV ({CvId})", education.Id, cv.Id);

            return _mapper.Map<EducationDto>(education);
        }
    }
}