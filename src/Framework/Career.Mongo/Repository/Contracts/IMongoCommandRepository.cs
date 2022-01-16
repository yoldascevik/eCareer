using Career.Repositories.Repository;

namespace Career.Mongo.Repository.Contracts;

public interface IMongoCommandRepository<T> : ICommandRepository<T> where T : class
{
}