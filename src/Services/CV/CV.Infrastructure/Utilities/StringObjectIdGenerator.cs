using Career.Shared.Generators;
using MongoDB.Bson;

namespace CurriculumVitae.Infrastructure.Utilities
{
    public class StringObjectIdGenerator : IStringIdGenerator
    {
        public string Generate()
        {
            return ObjectId.GenerateNewId().ToString();
        }
    }
}