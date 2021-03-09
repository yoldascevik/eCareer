using Career.Mongo.Mapping;
using Job.Domain.JobAdvertAggregate;
using Job.Domain.JobAdvertAggregate.Constants;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Job.Infrastructure.ClassMaps
{
    public class JobAdvertClassMap : MongoDbClassMap<JobAdvert>
    {
        protected override void Map(BsonClassMap<JobAdvert> map)
        {
            map.AutoMap();
            map.MapIdProperty(c => c.Id);
            map.MapProperty(c => c.Gender).SetSerializer(new EnumSerializer<GenderType>(BsonType.String));
            
            map.MapField("_tags").SetElementName("Tags");
            map.MapField("_locations").SetElementName("Locations");
            map.MapField("_workTypes").SetElementName("WorkTypes");
            map.MapField("_applications").SetElementName("Applications");
            map.MapField("_educationLevels").SetElementName("EducationLevels");
            map.MapField("_viewingHistories").SetElementName("ViewingHistories");
        }
    }
} 