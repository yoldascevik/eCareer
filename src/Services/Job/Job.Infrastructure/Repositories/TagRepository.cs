using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Career.Exceptions;
using Career.Mongo.Repository.Contracts;
using Job.Domain.TagAggregate;
using Job.Domain.TagAggregate.Repositories;

namespace Job.Infrastructure.Repositories
{
    public class TagRepository: ITagRepository
    {
        private readonly IMongoRepository<Tag> _repository;
        
        public TagRepository(IMongoRepository<Tag> repository)
        {
            Check.NotNull(repository, nameof(repository));
            _repository = repository;
        }

        public IQueryable<Tag> Get()
            => _repository.Get();

        public async Task<Tag> GetByIdAsync(Guid tagId)
            => await _repository.GetByKeyAsync(tagId);

        public async Task<bool> AnyAsync(Expression<Func<Tag, bool>> condition)
            => await _repository.AnyAsync(condition);

        public async Task<Tag> AddAsync(Tag tag)
            => await _repository.AddAsync(tag);

        public async Task<Tag> UpdateAsync(Guid tagId, Tag tag)
            => await _repository.UpdateAsync(tagId, tag);
    }
}