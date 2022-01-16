using System;
using System.Linq.Expressions;

namespace Career.Domain.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> ToExpression();
    bool IsSatisfiedBy(T obj);
}