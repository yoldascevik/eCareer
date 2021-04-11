using Career.Mongo.Mapping;
using Job.Domain.TagAggregate;
using MongoDB.Bson.Serialization;

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