using System;
using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http;
using Microsoft.AspNetCore.Http;

namespace Company.HttpClient.CompanyFollower
{
    public class CompanyFollowerHttpClient: CareerHttpClient, ICompanyFollowerHttpClient
    {
        private readonly ApiEndpointOptions _apiEndpointOptions;

        public CompanyFollowerHttpClient(System.Net.Http.HttpClient httpClient, IHttpContextAccessor httpContext, 
            ApiEndpointOptions apiEndpointOptions) : base(httpClient, httpContext)
        {
            _apiEndpointOptions = apiEndpointOptions;
        }

        // GET api/v{version}/company-followers/{userId}/companies
        public async Task<ConsistentApiResponse<PagedList<Guid>>> Get(Guid userId, PaginationFilter paginationFilter, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<PagedList<Guid>>>(CreateUrl($"/{userId}/companies", version), paginationFilter);
        }

        // POST api/v{version}/company-followers/{userId}/follow/{companyId}
        public async Task<ConsistentApiResponse> FollowCompany(Guid userId, Guid companyId, string version = null)
        {
            return await PostAsync<ConsistentApiResponse>(CreateUrl($"/{userId}/follow/{companyId}", version), null);
        }

        // POST api/v{version}/company-followers/{userId}/unfollow/{companyId}
        public async Task<ConsistentApiResponse> UnfollowCompany(Guid userId, Guid companyId, string version = null)
        {
            return await PostAsync<ConsistentApiResponse>(CreateUrl($"/{userId}/unfollow/{companyId}", version), null);
        }

        private string CreateUrl(string requestPath, string version)
        {
            if (string.IsNullOrEmpty(version))
                version = _apiEndpointOptions.DefaultVersion;

            return $"{_apiEndpointOptions.ApiUrl}/api/v{version}/company-followers{requestPath ?? string.Empty}";
        }
    }
}