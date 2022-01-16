using System.Linq.Expressions;
using Career.Exceptions;

namespace Career.Domain.Specifications.Logical;

public class And<T> : Specification<T>
{
    private readonly ISpecification<T> _left;
    private readonly ISpecification<T> _right;

    public And(ISpecification<T> left, ISpecification<T> right)
    {
        Check.NotNull(left, nameof(left));
        Check.NotNull(right, nameof(right));

        _left = left;
        _right = right;
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        ParameterExpression objParam = Expression.Parameter(typeof(T), "obj");

        var newExpr = Expression.Lambda<Func<T, bool>>(
            Expression.AndAlso(
                Expression.Invoke(_left.ToExpression(), objParam),
                Expression.Invoke(_right.ToExpression(), objParam)
            ),
            objParam
        );

        return newExpr;
    }
}