using Career.Mongo.Mapping;
using Job.Domain.JobApplicationAggregate;
using MongoDB.Bson.Serialization;

namespace Job.Infrastructure.ClassMaps
{
    public class JobApplicationClassMap : MongoDbClassMap<JobApplication>
    {
        protected override void Map(BsonClassMap<JobApplication> map)
        {
            map.AutoMap();
            map.MapIdProperty(c => c.Id);
        }
    }
} 