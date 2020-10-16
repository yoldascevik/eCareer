using Career.Repositories.Repository;

namespace Career.Mongo.Repository.Contracts
{
    public interface IMongoQueryRepository<T> : IQueryRepository<T> where T : class
    {
    }
}
