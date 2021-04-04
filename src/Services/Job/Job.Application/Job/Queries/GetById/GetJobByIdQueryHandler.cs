using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Query;
using Job.Application.Job.Dtos;
using Job.Application.Job.Exceptions;
using Job.Domain.JobAggregate.Repositories;

namespace Job.Application.Job.Queries.GetById
{
    public class GetJobByIdQueryHandler: IQueryHandler<GetJobByIdQuery, JobDetailDto>
    {
        private readonly IMapper _mapper;
        private readonly IJobRepository _jobRepository;

        public GetJobByIdQueryHandler(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }

        public async Task<JobDetailDto> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job == null)
                throw new JobNotFoundException(request.JobId);
            
            return _mapper.Map<JobDetailDto>(job);
        }
    }
}