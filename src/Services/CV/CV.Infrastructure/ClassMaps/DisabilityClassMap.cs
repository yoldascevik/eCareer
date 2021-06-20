using Career.Mongo.Mapping;
using CurriculumVitae.Core.Entities;
using MongoDB.Bson.Serialization;

namespace CurriculumVitae.Infrastructure.ClassMaps
{
    public class DisabilityClassMap : MongoDbClassMap<Disability>
    {
        protected override void Map(BsonClassMap<Disability> map)
        {
            map.AutoMap();
        }
    }
}