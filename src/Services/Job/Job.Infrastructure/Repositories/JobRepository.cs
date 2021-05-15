using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Career.Exceptions;
using Career.Mongo.Context;
using Career.Mongo.Repository.Contracts;
using Job.Domain.JobAggregate.Constants;
using Job.Domain.JobAggregate.Refs;
using Job.Domain.JobAggregate.Repositories;
using MongoDB.Driver;
using Tag = Job.Domain.TagAggregate.Tag;

namespace Job.Infrastructure.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly IMongoCollection<Domain.JobAggregate.Job> _collection;
        private readonly IMongoRepository<Domain.JobAggregate.Job> _repository;
        
        public JobRepository(IMongoContext context, IMongoRepository<Domain.JobAggregate.Job> repository)
        {
            Check.NotNull(context, nameof(context));
            Check.NotNull(repository, nameof(repository));
            
            _repository = repository;
            _collection = context.Database.GetCollection<Domain.JobAggregate.Job>(nameof(Domain.JobAggregate.Job));
        }
        
        public async Task<IEnumerable<Domain.JobAggregate.Job>> GetByTagAsync(Tag tag)
        {
            var filter = Builders<Domain.JobAggregate.Job>.Filter
                .ElemMatch(nameof(Domain.JobAggregate.Job.Tags), Builders<TagRef>.Filter.Eq(t => t.TagId, tag.Id));
            
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<bool> IsCandidateExistsAsync(Guid jobId, Guid userId)
        {
            var job = await GetByIdAsync(jobId);
            if (job == null)
                return false;

            return job.Candidates.Any(c => c.UserId == userId);
        }

        public IQueryable<Domain.JobAggregate.Job> Get()
            => _repository.Get(x=> !x.IsDeleted);

        public IQueryable<Domain.JobAggregate.Job> Get(Expression<Func<Domain.JobAggregate.Job, bool>> condition)
            => _repository.Get(condition);

        public IQueryable<Domain.JobAggregate.Job> GetPublishedJobs()
            => _repository.Get(x => x.Status == JobStatus.Published && !x.IsDeleted);

        public async Task<Domain.JobAggregate.Job> GetByIdAsync(Guid jobId)
            => await _repository.GetByKeyAsync(jobId);

        public async Task<Domain.JobAggregate.Job> AddAsync(Domain.JobAggregate.Job job)
            => await _repository.AddAsync(job);

        public async Task<Domain.JobAggregate.Job> UpdateAsync(Guid jobId, Domain.JobAggregate.Job job)
            => await _repository.UpdateAsync(jobId, job);

        public async Task UpdateCompanyNamesInJob(Guid companyId, string companyName)
        {
            var filter = Builders<Domain.JobAggregate.Job>.Filter.Eq(x=> x.Company.RefId, companyId);
            var update = Builders<Domain.JobAggregate.Job>.Update.Set(x => x.Company.Name, companyName);
            
            await _collection.UpdateManyAsync(filter, update);
        }
    }
}