using Career.Mongo.Mapping;
using CurriculumVitae.Core.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace CurriculumVitae.Infrastructure.ClassMaps;

public class EntityBaseClassMap : MongoDbClassMap<EntityBase>
{
    protected override void Map(BsonClassMap<EntityBase> map)
    {
        map.AutoMap();
        map.MapIdProperty(c => c.Id)
            .SetIdGenerator(StringObjectIdGenerator.Instance)
            .SetSerializer(new StringSerializer(BsonType.ObjectId));
    }
}