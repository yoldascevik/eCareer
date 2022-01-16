using Career.Mongo.Context;
using MongoDB.Driver;

namespace Definition.Data;

public class DefinitionContext : MongoContext
{
    public DefinitionContext(IMongoDatabase database) 
        : base(database)
    {
    }
}