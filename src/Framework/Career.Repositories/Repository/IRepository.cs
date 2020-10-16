namespace Career.Repositories.Repository
{
    public interface IRepository<T> : IQueryRepository<T>, ICommandRepository<T> where T : class
    {
    }
}