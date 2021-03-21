using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Career.Data.Pagination;
using Career.MediatR.Query;
using Job.Application.Job.Dtos;
using Job.Domain.JobAggregate.Repositories;

namespace Job.Application.Job.Queries.GetJobs
{
    public class GetJobsQueryHandler : IQueryHandler<GetJobsQuery, PagedList<JobDto>>
    {
        private readonly IMapper _mapper;
        private readonly IJobRepository _jobRepository;

        public GetJobsQueryHandler(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }

        public async Task<PagedList<JobDto>> Handle(GetJobsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.JobAggregate.Job> jobs = request.IsOnlyActive
                ? _jobRepository.GetActiveJobs()
                : _jobRepository.Get(x => !x.IsDeleted);

            return await jobs.ProjectTo<JobDto>(_mapper.ConfigurationProvider).ToPagedListAsync(request);
        }
    }
}