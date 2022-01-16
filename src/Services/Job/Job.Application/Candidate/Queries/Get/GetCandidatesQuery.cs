using Career.Data.Pagination;
using Career.MediatR.Query;
using Job.Application.Candidate.Dtos;

namespace Job.Application.Candidate.Queries.Get;

public class GetCandidatesQuery:  PaginationFilter, IQuery<PagedList<CandidateDto>>
{
    public bool IncludeDeactivated { get; set; }
}