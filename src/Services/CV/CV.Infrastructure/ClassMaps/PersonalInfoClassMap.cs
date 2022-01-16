using Career.Mongo.Mapping;
using CurriculumVitae.Core.Constants;
using CurriculumVitae.Core.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace CurriculumVitae.Infrastructure.ClassMaps;

public class PersonalInfoClassMap : MongoDbClassMap<PersonalInfo>
{
    protected override void Map(BsonClassMap<PersonalInfo> map)
    {
        map.AutoMap();
        map.MapProperty(c => c.Gender).SetSerializer(new EnumSerializer<GenderType>(BsonType.String));
    }
}