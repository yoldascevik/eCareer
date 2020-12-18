using System;
using System.Linq.Expressions;

namespace Career.Domain.Specifications.Logical
{
    public class Not<T> : Specification<T>
    {
        private readonly ISpecification<T> _inner;

        public Not(ISpecification<T> inner)
        {
            _inner = inner;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            ParameterExpression objParam = Expression.Parameter(typeof(T), "obj");

            var newExpr = Expression.Lambda<Func<T, bool>>(
                Expression.Not(
                    Expression.Invoke(_inner.ToExpression(), objParam)
                ),
                objParam
            );

            return newExpr;
        }
    }
}
