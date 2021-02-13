using Career.Mongo.Mapping;
using Job.Domain.Constants;
using Job.Domain.JobAggregate;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Job.Infrastructure.Mongo.ClassMaps
{
    public class JobAdvertClassMap : MongoDbClassMap<JobAdvert>
    {
        protected override void Map(BsonClassMap<JobAdvert> map)
        {
            map.AutoMap();
            map.MapIdProperty(c => c.Id);
            map.MapProperty(c => c.Gender).SetSerializer(new EnumSerializer<GenderType>(BsonType.String));
        }
    }
} 