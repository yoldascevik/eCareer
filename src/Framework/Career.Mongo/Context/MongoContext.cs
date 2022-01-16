using MongoDB.Driver;

namespace Career.Mongo.Context;

public abstract class MongoContext : IMongoContext
{
    protected MongoContext(IMongoDatabase database)
    {
        Database = database;
    }

    public IMongoDatabase Database { get; }
}