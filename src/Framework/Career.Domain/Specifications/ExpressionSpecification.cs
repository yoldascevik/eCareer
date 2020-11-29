using System;
using System.Linq.Expressions;

namespace Career.Domain.Specifications
{
    public abstract class ExpressionSpecification<T> : IExpressionSpecification<T>
    {
        public virtual bool IsSatisfiedBy(T obj)
        {
            return ToExpression().Compile()(obj);
        }
        
        public abstract Expression<Func<T, bool>> ToExpression();

        public static implicit operator Expression<Func<T, bool>>(ExpressionSpecification<T> expressionSpecification)
        {
            return expressionSpecification.ToExpression();
        }
    }
}