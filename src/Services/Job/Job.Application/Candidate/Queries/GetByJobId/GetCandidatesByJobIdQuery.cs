using System;
using Career.Data.Pagination;
using Career.MediatR.Query;
using Job.Application.Candidate.Dtos;

namespace Job.Application.Candidate.Queries.GetByJobId
{
    public class GetCandidatesByJobIdQuery: PaginationFilter, IQuery<PagedList<CandidateDto>>
    {
        public Guid JobId { get; set; }
        public bool IncludeDeactivated{ get; set; }
    }
}