using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Command;
using CurriculumVitae.Application.Cv.Dtos;
using CurriculumVitae.Core.Entities;
using CurriculumVitae.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.Cv.Commands.Create
{
    public class CreateCVCommandHandler: ICommandHandler<CreateCVCommand, CVSummaryDto>
    {
        private readonly IMapper _mapper;
        private readonly ICVRepository _cvRepository;
        private readonly ILogger<CreateCVCommandHandler> _logger;
        
        public CreateCVCommandHandler(
            ICVRepository cvRepository, 
            IMapper mapper,
            ILogger<CreateCVCommandHandler> logger) 
        {
            _cvRepository = cvRepository;
            _logger = logger;
            _mapper = mapper;
        }
        
        public async Task<CVSummaryDto> Handle(CreateCVCommand request, CancellationToken cancellationToken)
        {
            var cv = new CV()
            {
                UserId = request.UserId,
                PersonalInfo = _mapper.Map<Core.Entities.PersonalInfo>(request.PersonalInfo),
                Location = _mapper.Map<PersonLocation>(request.Location)
            };

            await _cvRepository.AddAsync(cv);
            _logger.LogInformation("CV created: {FirstName} {LastName}", cv.PersonalInfo.FirstName, cv.PersonalInfo.LastName);

            return _mapper.Map<CVSummaryDto>(cv);
        }
    }
}