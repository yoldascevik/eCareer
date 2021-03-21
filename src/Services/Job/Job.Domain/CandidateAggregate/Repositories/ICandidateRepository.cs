using System;
using System.Threading.Tasks;

namespace Job.Domain.CandidateAggregate.Repositories
{
    public interface ICandidateRepository
    {
        Task<Candidate> GetByIdAsync(Guid candidateId);
        Task<Candidate> AddAsync(Candidate candidate);
        Task<Candidate> UpdateAsync(Guid candidateId, Candidate candidate);
        Task<bool> IsCandidateExists(Guid userId, Guid jobId);
    }
}