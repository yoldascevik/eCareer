using Career.Http;
using Microsoft.AspNetCore.Http;

namespace Company.HttpClient.Company
{
    public class CompanyHttpClient : CareerHttpClient, ICompanyHttpClient
    {
        private readonly ApiEndpointOptions _apiEndpointOptions;
        
        public CompanyHttpClient(System.Net.Http.HttpClient httpClient, IHttpContextAccessor httpContext, 
            ApiEndpointOptions apiEndpointOptions) : base(httpClient, httpContext)
        {
            _apiEndpointOptions = apiEndpointOptions;
        }
    }
}