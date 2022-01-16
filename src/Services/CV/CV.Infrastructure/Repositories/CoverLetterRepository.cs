using Career.Domain.Extensions;
using Career.EventHub;
using Career.Mongo.Context;
using Career.Mongo.Repository;
using CurriculumVitae.Core.Entities;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Infrastructure.Repositories;

public class CoverLetterRepository : MongoRepository<CoverLetter>, ICoverLetterRepository
{
    public CoverLetterRepository(IMongoContext context, IEventDispatcher domainEventDispatcher) 
        : base(context, domainEventDispatcher)
    {
    }

    public IQueryable<CoverLetter> GetByUserId(Guid userId)
    {
        return Get(x => x.UserId == userId).ExcludeDeletedItems();
    }
}