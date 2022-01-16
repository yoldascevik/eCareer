using Career.Mongo.Mapping;
using Job.Domain.CandidateAggregate;
using MongoDB.Bson.Serialization;

namespace Job.Infrastructure.ClassMaps;

public class CandidateClassMap : MongoDbClassMap<Candidate>
{
    protected override void Map(BsonClassMap<Candidate> map)
    {
        map.AutoMap();
        map.MapIdProperty(c => c.Id);
    }
}