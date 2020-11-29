using System;
using System.Linq.Expressions;

namespace Career.Domain.Specifications
{
    public interface IExpressionSpecification<T> : ISpecification<T>
    {
        Expression<Func<T, bool>> ToExpression();
    }
}