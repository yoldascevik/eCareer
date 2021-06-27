using System.Threading.Tasks;
using Career.EventHub;
using Career.Mongo.Context;
using Career.Mongo.Repository;
using CurriculumVitae.Core.Entities;
using CurriculumVitae.Core.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CurriculumVitae.Infrastructure.Repositories
{
    public class CVRepository : MongoRepository<CV>, ICVRepository
    {
        public CVRepository(IMongoContext context, IEventDispatcher domainEventDispatcher)
            : base(context, domainEventDispatcher)
        {
        }

        public async Task UpdateAllDisabilityTypeNamesInCV(DisabilityType disabilityType)
        {
            var filter = Builders<CV>.Filter.ElemMatch(cv => cv.PersonalInfo.Disabilities, Builders<Disability>.Filter.Eq(x => x.Type.Id, disabilityType.Id));
            var update = Builders<CV>.Update.Set("PersonalInfo.Disabilities.$[d].Type.Name", disabilityType.Name);
            var updateOptions = new UpdateOptions()
            {
                ArrayFilters = new []
                {
                    new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("d.Type._id", new ObjectId(disabilityType.Id)))
                }
            };

            await Collection.UpdateManyAsync(filter, update, updateOptions);
        }

        public async Task UpdateAllSocialProfileTypesInCV(SocialProfileType socialProfileType)
        {
            var filter = Builders<CV>.Filter.ElemMatch(cv => cv.SocialProfiles, Builders<SocialProfile>.Filter.Eq(x => x.Type.Id, socialProfileType.Id));
            var update = Builders<CV>.Update
                .Set("SocialProfiles.$[s].Type.Name", socialProfileType.Name)
                .Set("SocialProfiles.$[s].Type.ProfileUrlPrefix", socialProfileType.ProfileUrlPrefix);
            
            var updateOptions = new UpdateOptions()
            {
                ArrayFilters = new []
                {
                    new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("s.Type._id", new ObjectId(socialProfileType.Id)))
                }
            };
            
            await Collection.UpdateManyAsync(filter, update, updateOptions);
        }
    }
}