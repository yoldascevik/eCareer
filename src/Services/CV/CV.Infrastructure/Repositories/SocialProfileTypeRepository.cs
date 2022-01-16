using Career.EventHub;
using Career.Mongo.Context;
using Career.Mongo.Repository;
using CurriculumVitae.Core.Entities;
using CurriculumVitae.Core.Events;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Infrastructure.Repositories;

public class SocialProfileTypeRepository : MongoRepository<SocialProfileType>, ISocialProfileTypeRepository
{
    private readonly IEventDispatcher _domainEventDispatcher;
        
    public SocialProfileTypeRepository(IMongoContext context, IEventDispatcher domainEventDispatcher) 
        : base(context, domainEventDispatcher)
    {
        _domainEventDispatcher = domainEventDispatcher;
    }

    public async Task<bool> ExistsByNameAsync(string name)
        => await AnyAsync(x => x.Name.ToLowerInvariant() == name.ToLowerInvariant() && !x.IsDeleted);

    public async Task<bool> ExistsByNameAsync(string name, string excludeId)
        => await AnyAsync(x => x.Name.ToLowerInvariant() == name.ToLowerInvariant() && !x.IsDeleted && x.Id != excludeId);

    public override SocialProfileType Update(object key, SocialProfileType item)
    {
        var oldSocialProfileType = GetByKey(key); 
        var @event = new SocialProfileTypeUpdatedEvent(oldSocialProfileType);
            
        SocialProfileType socialProfileType = base.Update(key, item);

        @event.NewSocialProfileType = socialProfileType;
        _domainEventDispatcher.Dispatch(@event);
            
        return socialProfileType;
    }

    public override async Task<SocialProfileType> UpdateAsync(object key, SocialProfileType item)
    {
        var oldSocialProfileType = await GetByKeyAsync(key); 
        var @event = new SocialProfileTypeUpdatedEvent(oldSocialProfileType);
            
        SocialProfileType socialProfileType = await base.UpdateAsync(key, item);

        @event.NewSocialProfileType = socialProfileType;
        await _domainEventDispatcher.Dispatch(@event);
            
        return socialProfileType;
    }
}