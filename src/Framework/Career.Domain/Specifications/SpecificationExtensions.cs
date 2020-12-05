using Career.Domain.Specifications.Logical;

namespace Career.Domain.Specifications
{
    public static class SpecificationExtensions
    {
        public static ISpecification<T> And<T>(this ISpecification<T> left, ISpecification<T> right)
        {
            return new And<T>(left, right);
        }

        public static ISpecification<T> Or<T>(this ISpecification<T> left, ISpecification<T> right)
        {
            return new Or<T>(left, right);
        }

        public static ISpecification<T> Not<T>(this ISpecification<T> inner)
        {
            return new Not<T>(inner);
        }
    }
}