using Career.Mongo.Mapping;
using Job.Domain.JobAdvertAggregate;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Job.Infrastructure.ClassMaps
{
    public class TagClassMap : MongoDbClassMap<Tag>
    {
        protected override void Map(BsonClassMap<Tag> map)
        {
            map.AutoMap();
            map.MapIdProperty(c => c.Id);
        }
    }
} 