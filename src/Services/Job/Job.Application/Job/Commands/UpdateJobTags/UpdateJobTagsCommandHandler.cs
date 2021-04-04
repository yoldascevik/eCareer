using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Command;
using Job.Application.Job.Exceptions;
using Job.Application.Tag.Dtos;
using Job.Domain.JobAggregate.Repositories;
using Job.Domain.JobAggregate.Services;
using Microsoft.Extensions.Logging;

namespace Job.Application.Job.Commands.UpdateJobTags
{
    public class UpdateJobTagsCommandHandler: ICommandHandler<UpdateJobTagsCommand, TagDto>
    {
        private readonly IMapper _mapper;
        private readonly IJobRepository _jobRepository;
        private readonly IJobDomainService _jobDomainService;
        private readonly ILogger<UpdateJobTagsCommandHandler> _logger;

        public UpdateJobTagsCommandHandler(
            IMapper mapper, 
            IJobRepository jobRepository, 
            IJobDomainService jobDomainService, 
            ILogger<UpdateJobTagsCommandHandler> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _jobRepository = jobRepository;
            _jobDomainService = jobDomainService;
        }

        public async Task<TagDto> Handle(UpdateJobTagsCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job is null)
                throw new JobNotFoundException(request.JobId);

            await _jobDomainService.UpdateTagsAsync(job, request.Tags);
            _logger.LogInformation("Tags updated to job: {JobId}", job.Id);

            return _mapper.Map<TagDto>(job.Tags);
        }
    }
}