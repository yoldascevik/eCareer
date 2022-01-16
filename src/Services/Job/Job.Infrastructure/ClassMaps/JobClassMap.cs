using Career.Mongo.Mapping;
using Job.Domain.JobAggregate.Constants;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Job.Infrastructure.ClassMaps;

public class JobClassMap : MongoDbClassMap<Domain.JobAggregate.Job>
{
    protected override void Map(BsonClassMap<Domain.JobAggregate.Job> map)
    {
        map.AutoMap();
        map.MapIdProperty(c => c.Id);
        map.MapProperty(c => c.Gender).SetSerializer(new EnumSerializer<GenderType>(BsonType.String));
            
        map.MapField("_tags").SetElementName("Tags");
        map.MapField("_locations").SetElementName("Locations");
        map.MapField("_workTypes").SetElementName("WorkTypes");
        map.MapField("_candidates").SetElementName("Candidates");
        map.MapField("_educationLevels").SetElementName("EducationLevels");
        map.MapField("_viewingHistories").SetElementName("ViewingHistories");
    }
}