using System.Threading.Tasks;
using Career.EventHub;
using Career.Mongo.Context;
using Career.Mongo.Repository;
using CurriculumVitae.Core.Entities;
using CurriculumVitae.Core.Repositories;
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
            var update = Builders<CV>.Update.Set(cv => ((Disability[]) cv.PersonalInfo.Disabilities)[-1].Type.Name, disabilityType.Name);

            await Collection.UpdateManyAsync(filter, update);
        }

        public async Task UpdateAllSocialProfileTypesInCV(SocialProfileType socialProfileType)
        {
            var filter = Builders<CV>.Filter.ElemMatch(cv => cv.SocialProfiles, Builders<SocialProfile>.Filter.Eq(x => x.Type.Id, socialProfileType.Id));
            var update = Builders<CV>.Update
                .Set(cv => ((SocialProfile[]) cv.SocialProfiles)[-1].Type.Name, socialProfileType.Name)
                .Set(cv => ((SocialProfile[]) cv.SocialProfiles)[-1].Type.ProfileUrlPrefix, socialProfileType.ProfileUrlPrefix);
            
            await Collection.UpdateManyAsync(filter, update);
        }
    }
}