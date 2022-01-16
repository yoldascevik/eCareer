using System;
using System.Linq;
using System.Threading.Tasks;

namespace Job.Domain.CandidateAggregate.Repositories;

public interface ICandidateRepository
{
    IQueryable<Candidate> Get(bool includeDeactivated);
    Task<Candidate> GetByIdAsync(Guid candidateId);
    IQueryable<Candidate> GetByJobId(Guid jobId, bool includeDeactivated);
    Task<Candidate> AddAsync(Candidate candidate);
    Task<Candidate> UpdateAsync(Guid candidateId, Candidate candidate);
}