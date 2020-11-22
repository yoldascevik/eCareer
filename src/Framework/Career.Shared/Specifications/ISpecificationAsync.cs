using System.Threading.Tasks;

namespace Career.Shared.Specifications
{
    public interface ISpecificationAsync<T>
    {
        Task<bool> IsSatisfiedByAsync(T entity);
    }
}