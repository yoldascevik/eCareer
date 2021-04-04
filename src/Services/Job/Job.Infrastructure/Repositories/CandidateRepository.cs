using System;
using System.Linq;
using System.Threading.Tasks;
using Career.Exceptions;
using Career.Mongo.Repository.Contracts;
using Job.Domain.CandidateAggregate;
using Job.Domain.CandidateAggregate.Repositories;

namespace Job.Infrastructure.Repositories
{
    public class CandidateRepository: ICandidateRepository
    {
        private readonly IMongoRepository<Candidate> _repository;
        
        public CandidateRepository(IMongoRepository<Candidate> repository)
        {
            Check.NotNull(repository, nameof(repository));
            _repository = repository;
        }

        public IQueryable<Candidate> Get(bool includeDeactivated)
            => _repository.Get(x => !includeDeactivated || x.IsActive);
        
        public async Task<Candidate> GetByIdAsync(Guid candidateId)
            => await _repository.GetByKeyAsync(candidateId);

        public IQueryable<Candidate> GetByJobId(Guid jobId, bool includeDeactivated)
            => _repository.Get(x => x.JobId == jobId && (!includeDeactivated || x.IsActive));
        
        public async Task<Candidate> UpdateAsync(Guid candidateId, Candidate candidate)
            => await _repository.UpdateAsync(candidateId, candidate);

        async Task<Candidate> ICandidateRepository.AddAsync(Candidate candidate)
            => await _repository.AddAsync(candidate);
    }
}