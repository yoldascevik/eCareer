using Career.EventHub;
using Career.Mongo.Context;
using Career.Mongo.Repository;
using CurriculumVitae.Core.Entities;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Infrastructure.Repositories;

public class AttachmentRepository : MongoRepository<Attachment>, IAttachmentRepository
{
    public AttachmentRepository(IMongoContext context, IEventDispatcher domainEventDispatcher) 
        : base(context, domainEventDispatcher)
    {
    }
}