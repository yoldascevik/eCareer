using System.Linq.Expressions;
using System.Reflection;

namespace Career.Data.Pagination.Helpers;

internal static class QueryableExtensions
{
    internal static IQueryable<T> OrderByKeyIfNecessary<T>(this IQueryable<T> query)
    {
        if (query == null)
            throw new ArgumentNullException(nameof(query));

        // not necessary
        if (OrderingExpressionVisitor.OrderMethodExists(query.Expression))
            return query;
            
        var type = typeof(T);
        PropertyInfo? keyField = typeof(T).GetProperty("Id");

        if (keyField != null)
        {
            var parameter = Expression.Parameter(type, "x");
            var property = Expression.Property(parameter, keyField);
            var lambda = Expression.Lambda(property, parameter);

            var orderByCall = Expression.Call(
                typeof(Queryable),
                "OrderBy",
                new[] {type, keyField.PropertyType},
                query.Expression,
                lambda
            );

            return query.Provider.CreateQuery<T>(orderByCall);
        }

        return query;
    }
}