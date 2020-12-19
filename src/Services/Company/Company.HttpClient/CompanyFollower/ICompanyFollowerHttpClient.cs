using System;
using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http;

namespace Company.HttpClient.CompanyFollower
{
    public interface ICompanyFollowerHttpClient : ICareerHttpClient
    {
        /// <summary>
        /// Get all followed company of user
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="paginationFilter">Paging configuration</param>
        /// <param name="version">Api version. Default = 1</param>
        /// <returns>Company id list with pagination</returns>
        Task<ConsistentApiResponse<PagedList<Guid>>> Get(Guid userId, PaginationFilter paginationFilter, string version = null);

        /// <summary>
        /// Follow company
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="companyId">Company Id</param>
        /// <param name="version">Api version. Default = 1</param>
        Task<ConsistentApiResponse> FollowCompany(Guid userId, Guid companyId, string version = null);
        
        /// <summary>
        /// Unfollow company
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="companyId">Company Id</param>
        /// <param name="version">Api version. Default = 1</param>
        Task<ConsistentApiResponse> UnfollowCompany(Guid userId, Guid companyId, string version = null);
    }
}