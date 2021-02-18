using Career.Mongo.Mapping;
using Job.Domain.JobAdvertAggregate;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Job.Infrastructure.Mongo.ClassMaps
{
    public class TagClassMap : MongoDbClassMap<Tag>
    {
        protected override void Map(BsonClassMap<Tag> map)
        {
            map.AutoMap();
            map.SetIgnoreExtraElements(true);
            map.MapIdField(x => x.Id).SetIdGenerator(ObjectIdGenerator.Instance);
            map.MapMember(x => x.Name).SetIsRequired(true);
        }
    }
} 