using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Command;
using Job.Application.Job.Dtos;
using Job.Application.Job.Exceptions;
using Job.Domain.JobAggregate.Repositories;
using Microsoft.Extensions.Logging;

namespace Job.Application.Job.Commands.Update
{
    public class UpdateJobCommandHandler: ICommandHandler<UpdateJobCommand, JobDetailDto>
    {
        private readonly IMapper _mapper;
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<UpdateJobCommandHandler> _logger;

        public UpdateJobCommandHandler(IJobRepository jobRepository, IMapper mapper, ILogger<UpdateJobCommandHandler> logger)
        {
            _jobRepository = jobRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<JobDetailDto> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job is null)
                throw new JobNotFoundException(request.JobId);

            job.SetTitle(request.Job.Title)
                .SetDescription(request.Job.Description)
                .SetLanguage(request.Job.LanguageId)
                .SetGender(request.Job.Gender)
                .SetSector(request.Job.SectorId)
                .SetPersonCount(request.Job.PersonCount)
                .SetJobPosition(request.Job.JobPositionId)
                .SetCanDisabilities(request.Job.IsCanDisabilities)
                .SetMinExperienceYear(request.Job.MinExperienceYear)
                .SetMaxExperienceYear(request.Job.MaxExperienceYear);
            
            _logger.LogInformation("Job detail info updated : {JobId}", job.Id);
            
            return _mapper.Map<JobDetailDto>(job);
        }
    }
}