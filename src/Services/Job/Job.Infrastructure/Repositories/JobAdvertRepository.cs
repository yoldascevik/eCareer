using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Career.Exceptions;
using Career.Mongo.Context;
using Career.Mongo.Repository.Contracts;
using Job.Domain.JobAdvertAggregate;
using Job.Domain.JobAdvertAggregate.Repositories;
using MongoDB.Driver;
using Tag = Job.Domain.TagAggregate.Tag;

namespace Job.Infrastructure.Repositories
{
    public class JobAdvertRepository : IJobAdvertRepository
    {
        private readonly IMongoCollection<JobAdvert> _collection;
        private readonly IMongoRepository<JobAdvert> _repository;
        
        public JobAdvertRepository(IMongoContext context, IMongoRepository<JobAdvert> repository)
        {
            Check.NotNull(context, nameof(context));
            Check.NotNull(repository, nameof(repository));
            
            _repository = repository;
            _collection = context.Database.GetCollection<JobAdvert>(nameof(JobAdvert));
        }
        
        public async Task<IEnumerable<JobAdvert>> GetByTag(Tag tag)
        {
            var filter = Builders<JobAdvert>.Filter.ElemMatch(x => x.Tags, t=> t.TagId == tag.Id);
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<JobAdvert> GetByIdAsync(Guid jobAdvertId)
            => await _repository.GetByKeyAsync(jobAdvertId);

        public async Task<JobAdvert> AddAsync(JobAdvert jobAdvert)
            => await _repository.AddAsync(jobAdvert);

        public async Task<JobAdvert> UpdateAsync(Guid jobAdvertId, JobAdvert jobAdvert)
            => await _repository.UpdateAsync(jobAdvertId, jobAdvert);
    }
}