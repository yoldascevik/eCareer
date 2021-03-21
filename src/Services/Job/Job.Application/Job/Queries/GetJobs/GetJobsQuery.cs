using Career.Data.Pagination;
using Career.MediatR.Query;
using Job.Application.Job.Dtos;

namespace Job.Application.Job.Queries.GetJobs
{
    public class GetJobsQuery:  PaginationFilter, IQuery<PagedList<JobDto>>
    {
        /// <summary>
        /// Get only active jobs.
        /// </summary>
        public bool IsOnlyActive { get; set; }
    }
}