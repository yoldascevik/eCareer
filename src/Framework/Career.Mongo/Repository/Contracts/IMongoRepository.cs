using Career.Repositories.Repository;

namespace Career.Mongo.Repository.Contracts;

public interface IMongoRepository<T> : IRepository<T> where T : class
{
}