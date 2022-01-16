using Career.Mongo.Context;
using Career.Mongo.Mapping;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Job.Infrastructure;

public class JobDbContext : MongoContext
{
    public JobDbContext(IMongoDatabase database)
        : base(database)
    {
    }

    public static void Configure()
    {
#pragma warning disable 618
        BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
#pragma warning restore 618
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        BsonClassMappingConfiguration.ApplyConfigurationsFromAssembly(typeof(JobDbContext));
    }
}