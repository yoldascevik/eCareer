using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Career.Utilities.Pagination
{
    public static class PaginationExtension
    {
        public static Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize)
            => Task.FromResult(ToPagedList(query, new PaginationFilter(pageNumber, pageSize)));

        public static Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> query, PaginationFilter paginationFilter)
            => Task.FromResult(ToPagedList(query, paginationFilter));

        public static PagedList<T> ToPagedList<T>(this IQueryable<T> query, int pageNumber, int pageSize)
            => ToPagedList(query, new PaginationFilter(pageNumber, pageSize));

        public static PagedList<T> ToPagedList<T>(this IQueryable<T> query, PaginationFilter paginationFilter)
        {
            int skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            List<T> data = query.Skip(skip).Take(paginationFilter.PageSize).ToList();

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
}