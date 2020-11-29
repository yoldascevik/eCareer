namespace Career.Domain.Specifications
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T obj);
    }
}