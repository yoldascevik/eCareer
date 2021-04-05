using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Command;
using Job.Application.Job.Exceptions;
using Job.Application.Tag.Dtos;
using Job.Domain.JobAggregate.Repositories;
using Job.Domain.JobAggregate.Services;

namespace Job.Application.Job.Commands.UpdateJobTags
{
    public class UpdateJobTagsCommandHandler: ICommandHandler<UpdateJobTagsCommand, List<TagDto>>
    {
        private readonly IMapper _mapper;
        private readonly IJobRepository _jobRepository;
        private readonly IJobDomainService _jobDomainService;

        public UpdateJobTagsCommandHandler(
            IMapper mapper, 
            IJobRepository jobRepository, 
            IJobDomainService jobDomainService)
        {
            _mapper = mapper;
            _jobRepository = jobRepository;
            _jobDomainService = jobDomainService;
        }

        public async Task<List<TagDto>> Handle(UpdateJobTagsCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job is null)
                throw new JobNotFoundException(request.JobId);

            await _jobDomainService.UpdateTagsAsync(job, request.Tags);

            return _mapper.Map<List<TagDto>>(job.Tags);
        }
    }
}