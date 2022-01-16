using System;
using Career.Data.Pagination;
using Career.MediatR.Query;
using Job.Application.Candidate.Dtos;

namespace Job.Application.Candidate.Queries.GetByJobId;

public class GetCandidatesByJobIdQuery: IQuery<PagedList<CandidateDto>>
{
    public GetCandidatesByJobIdQuery(Guid jobId, bool includeDeactivated, PaginationFilter paginationFilter)
    {
        JobId = jobId;
        IncludeDeactivated = includeDeactivated;
        PaginationFilter = paginationFilter;
    }
        
    public Guid JobId { get; }
    public bool IncludeDeactivated{ get; }
    public PaginationFilter PaginationFilter { get; }
}