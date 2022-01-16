using MongoDB.Bson.Serialization;

namespace Career.Mongo.Mapping;

public abstract class MongoDbClassMap<T>
{
    protected MongoDbClassMap()
    {
        if (!BsonClassMap.IsClassMapRegistered(typeof(T)))
            BsonClassMap.RegisterClassMap<T>(Map);
    }

    protected abstract void Map(BsonClassMap<T> map);
}