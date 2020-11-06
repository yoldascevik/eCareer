using Career.Mongo.Document;
using Career.Shared.Interfaces;

namespace Definition.Data.Entities
{
    public abstract class LookupDocument : Document, ISoftDeletable
    {
        public bool IsDeleted { get; set; }
    }
}