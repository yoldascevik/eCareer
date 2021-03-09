using Career.Mongo.Mapping;
using Job.Domain.JobAdvertAggregate;
using Job.Domain.JobAdvertAggregate.Constants;
using Job.Domain.JobApplicationAggregate;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

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