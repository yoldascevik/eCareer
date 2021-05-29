using System.Collections.Generic;
using System.Linq;

namespace Career.Domain.Extensions
{
    public static class SoftDeletableExtensions
    {
        public static IQueryable<T> ExcludeDeletedItems<T>(this IQueryable<T> query) where T: ISoftDeletable
        {
            return query.Where(x => !x.IsDeleted);
        }
        
        public static ICollection<T> ExcludeDeletedItems<T>(this ICollection<T> collection) where T: ISoftDeletable
        {
            return collection.Where(x => !x.IsDeleted) as ICollection<T>;
        }
    }
}