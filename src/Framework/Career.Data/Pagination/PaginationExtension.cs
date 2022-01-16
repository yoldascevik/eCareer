using Career.Data.Pagination.Helpers;

namespace Career.Data.Pagination;

public static class PaginationExtension
{
    public static Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        => Task.FromResult(ToPagedList(query, new PaginationFilter(pageNumber, pageSize)));

    public static Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> query, PaginationFilter paginationFilter)
        => Task.FromResult(ToPagedList(query, paginationFilter));

    public static PagedList<T> ToPagedList<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        => ToPagedList(query, new PaginationFilter(pageNumber, pageSize));

    public static PagedList<T> ToPagedList<T>(this IEnumerable<T> collection, PaginationFilter paginationFilter)
        => ToPagedList(collection.AsQueryable(), paginationFilter);
        
    public static PagedList<T> ToPagedList<T>(this IQueryable<T> query, PaginationFilter paginationFilter)
    {
        if (query == null)
            throw new ArgumentNullException(nameof(query));

        if (paginationFilter == null)
            throw new ArgumentNullException(nameof(paginationFilter));

        int skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
        List<T> data = query
            .OrderByKeyIfNecessary()
            .Skip(skip)
            .Take(paginationFilter.PageSize)
            .ToList();
            
        int totalRecords = query.Count();
        int totalPages = totalRecords > 0 ? (int) Math.Ceiling((double) totalRecords / paginationFilter.PageSize) : 0;

        return new PagedList<T>(data)
        {
            PageNumber = paginationFilter.PageNumber,
            PageSize = paginationFilter.PageSize,
            TotalRecords = totalRecords,
            TotalPages = totalPages,
            NextPage = paginationFilter.PageNumber < totalPages ? paginationFilter.PageNumber + 1 : default(int?),
            PreviousPage = paginationFilter.PageNumber > 1 ? paginationFilter.PageNumber - 1 : default(int?)
        };
    }
}