using Career.Mongo.Mapping;
using Job.Domain.JobAggregate;
using MongoDB.Bson.Serialization;

namespace Job.Infrastructure.ClassMaps;

public class JobLocationClassMap : MongoDbClassMap<JobLocation>
{
    protected override void Map(BsonClassMap<JobLocation> map)
    {
        map.AutoMap();
        map.MapIdProperty(c => c.Id);
    }
}