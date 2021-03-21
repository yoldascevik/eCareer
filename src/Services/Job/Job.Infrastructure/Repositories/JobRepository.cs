using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Career.Exceptions;
using Career.Mongo.Context;
using Career.Mongo.Repository.Contracts;
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
        
        public async Task<IEnumerable<Domain.JobAggregate.Job>> GetByTag(Tag tag)
        {
            var filter = Builders<Domain.JobAggregate.Job>.Filter.ElemMatch(x => x.Tags, t=> t.TagId == tag.Id);
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<Domain.JobAggregate.Job> GetByIdAsync(Guid jobId)
            => await _repository.GetByKeyAsync(jobId);

        public async Task<Domain.JobAggregate.Job> AddAsync(Domain.JobAggregate.Job job)
            => await _repository.AddAsync(job);

        public async Task<Domain.JobAggregate.Job> UpdateAsync(Guid jobId, Domain.JobAggregate.Job job)
            => await _repository.UpdateAsync(jobId, job);
    }
}