using System;
using System.Threading.Tasks;
using Career.Exceptions;
using Career.Mongo.Repository.Contracts;
using Job.Domain.JobApplicationAggregate;
using Job.Domain.JobApplicationAggregate.Repositories;

namespace Job.Infrastructure.Repositories
{
    public class JobApplicationRepository: IJobApplicationRepository
    {
        private readonly IMongoRepository<JobApplication> _repository;
        
        public JobApplicationRepository(IMongoRepository<JobApplication> repository)
        {
            Check.NotNull(repository, nameof(repository));
            _repository = repository;
        }

        public async Task<JobApplication> GetByIdAsync(Guid jobApplicationId)
            => await _repository.GetByKeyAsync(jobApplicationId);
        
        public async Task<bool> IsJobApplicationExists(Guid userId, Guid jobAdvertId)
            => await _repository.AnyAsync(x => x.JobAdvertId == jobAdvertId && x.UserId == userId);
        
        public async Task<JobApplication> UpdateAsync(Guid jobApplicationId, JobApplication jobApplication)
            => await _repository.UpdateAsync(jobApplicationId, jobApplication);

        async Task<JobApplication> IJobApplicationRepository.AddAsync(JobApplication jobApplication)
            => await _repository.AddAsync(jobApplication);
    }
}