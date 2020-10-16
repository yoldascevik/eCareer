using MongoDB.Driver;

namespace Career.Mongo.Context
{
    public interface IMongoContext
    {
        IMongoDatabase Database { get; }
    }
}