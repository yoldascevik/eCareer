using Career.Domain;
using Career.Mongo.Document;

namespace Definition.Data.Entities;

public abstract class LookupDocument : Document, ISoftDeletable
{
    public bool IsDeleted { get; set; }
}