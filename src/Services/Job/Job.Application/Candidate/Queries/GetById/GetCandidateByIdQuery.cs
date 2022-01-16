using Career.MediatR.Query;
using Job.Application.Candidate.Dtos;

namespace Job.Application.Candidate.Queries.GetById;

public class GetCandidateByIdQuery: IQuery<CandidateDto>
{
    public GetCandidateByIdQuery(Guid candidateId)
    {
        CandidateId = candidateId;
    }

    public Guid CandidateId { get; }
}