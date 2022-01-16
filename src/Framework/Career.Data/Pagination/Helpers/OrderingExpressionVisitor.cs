using System;
using System.Linq;
using System.Linq.Expressions;

namespace Career.Data.Pagination.Helpers;

internal class OrderingExpressionVisitor : ExpressionVisitor
{
    private bool _orderingMethodFound;

    protected override Expression VisitMethodCall(MethodCallExpression node)
    {
        string name = node.Method.Name;

        if (node.Method.DeclaringType == typeof(Queryable) && (
                name.StartsWith("OrderBy", StringComparison.Ordinal) ||
                name.StartsWith("ThenBy", StringComparison.Ordinal)))
        {
            _orderingMethodFound = true;
        }

        return base.VisitMethodCall(node);
    }

    public static bool OrderMethodExists(Expression expression)
    {
        var visitor = new OrderingExpressionVisitor();
        visitor.Visit(expression);
        return visitor._orderingMethodFound;
    }
}