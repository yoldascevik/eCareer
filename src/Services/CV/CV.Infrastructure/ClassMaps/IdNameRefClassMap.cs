using Career.Mongo.Mapping;
using CurriculumVitae.Core.Refs;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace CurriculumVitae.Infrastructure.ClassMaps
{
    public class IdNameRefClassMap : MongoDbClassMap<IdNameRef>
    {
        protected override void Map(BsonClassMap<IdNameRef> map)
        {
            map.AutoMap();
            map.MapProperty(c => c.Id).SetSerializer(new StringSerializer(BsonType.ObjectId));
        }
    }
}