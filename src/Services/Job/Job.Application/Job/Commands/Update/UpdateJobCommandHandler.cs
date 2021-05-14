using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Command;
using Job.Application.Job.Dtos;
using Job.Application.Job.Exceptions;
using Job.Domain.JobAggregate.Refs;
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

            var sector = SectorRef.Create(request.Job.Sector.RefId, request.Job.Sector.Name);
            var language = LanguageRef.Create(request.Job.Language.RefId, request.Job.Language.Name);
            var jobPosition = JobPositionRef.Create(request.Job.JobPosition.RefId, request.Job.JobPosition.Name);
            
            job.SetTitle(request.Job.Title)
                .SetSector(sector)
                .SetLanguage(language)
                .SetJobPosition(jobPosition)
                .SetGender(request.Job.Gender)
                .SetDescription(request.Job.Description)
                .SetPersonCount(request.Job.PersonCount)
                .SetCanDisabilities(request.Job.IsCanDisabilities)
                .SetMinExperienceYear(request.Job.MinExperienceYear)
                .SetMaxExperienceYear(request.Job.MaxExperienceYear);

            await _jobRepository.UpdateAsync(job.Id, job);
            _logger.LogInformation("Job detail info updated : {JobId}", job.Id);
            
            return _mapper.Map<JobDetailDto>(job);
        }
    }
}