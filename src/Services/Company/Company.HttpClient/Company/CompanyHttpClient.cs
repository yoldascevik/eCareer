using System;
using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http;
using Company.Application.Company.Commands.CreateCompany;
using Company.Application.Company.Dtos;
using Company.Application.Company.Queries.GetCompanies;
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
        
        // GET api/v{version}/companies
        public async Task<ConsistentApiResponse<PagedList<CompanyDto>>> Get(GetCompaniesQuery query, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<PagedList<CompanyDto>>>(CreateUrl(null, version), query);
        }

        // GET api/v{version}/companies/{id}
        public async Task<ConsistentApiResponse<CompanyDto>> GetById(Guid companyId, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<CompanyDto>>(CreateUrl($"/{companyId}", version));
        }

        // GET api/v{version}/companies/{id}/address
        public async Task<ConsistentApiResponse<AddressDto>> GetCompanyAddress(Guid companyId, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<AddressDto>>(CreateUrl($"/{companyId}/address", version));
        }

        // GET api/v{version}/companies/{id}/detail
        public async Task<ConsistentApiResponse<CompanyDetailDto>> GetCompanyDetails(Guid companyId, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<CompanyDetailDto>>(CreateUrl($"/{companyId}/detail", version));
        }

        // GET api/v{version}/companies/{id}/tax
        public async Task<ConsistentApiResponse<TaxDto>> GetCompanyTaxInfo(Guid companyId, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<TaxDto>>(CreateUrl($"/{companyId}/tax", version));
        }

        // POST api/v{version}/companies
        public async Task<ConsistentApiResponse<Guid>> Create(CreateCompanyCommand command, string version = null)
        {
            return await PostAsync<ConsistentApiResponse<Guid>>(CreateUrl(null, version), command);
        }

        // PUT api/v{version}/companies/{id}/tax
        public async Task<ConsistentApiResponse<TaxDto>> UpdateTaxInfo(Guid companyId, TaxDto taxInfo, string version = null)
        {
            return await PutAsync<ConsistentApiResponse<TaxDto>>(CreateUrl($"/{companyId}/tax", version), taxInfo);
        }

        // PUT api/v{version}/companies/{id}/address
        public async Task<ConsistentApiResponse<AddressDto>> UpdateAddress(Guid companyId, AddressDto address, string version = null)
        {
            return await PutAsync<ConsistentApiResponse<AddressDto>>(CreateUrl($"/{companyId}/address", version), address);
        }

        // PUT api/v{version}/companies/{id}/detail
        public async Task<ConsistentApiResponse<CompanyDetailDto>> UpdateDetail(Guid companyId, CompanyDetailDto detail, string version = null)
        {
            return await PutAsync<ConsistentApiResponse<CompanyDetailDto>>(CreateUrl($"/{companyId}/detail", version), detail);
        }

        // PUT api/v{version}/companies/{id}/email
        public async Task<ConsistentApiResponse> UpdateEmailAddress(Guid companyId, string emailAddress, string version = null)
        {
            return await PutAsync<ConsistentApiResponse>(CreateUrl($"/{companyId}/email", version), emailAddress);
        }

        // PUT api/v{version}/companies/{id}/name
        public async Task<ConsistentApiResponse> UpdateCompanyName(Guid companyId, string companyName, string version = null)
        {
            return await PutAsync<ConsistentApiResponse>(CreateUrl($"/{companyId}/name", version), companyName);
        }

        // DELETE api/v{version}/companies/{id}
        public async Task<ConsistentApiResponse> DeleteCompany(Guid companyId, string version = null)
        {
            return await DeleteAsync<ConsistentApiResponse>(CreateUrl($"/{companyId}", version));
        }

        // GET api/v{version}/companies/{id}/followers
        public async Task<ConsistentApiResponse<PagedList<Guid>>> GetCompanyFollowers(Guid companyId, PaginationFilter paginationFilter, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<PagedList<Guid>>>(CreateUrl($"/{companyId}", version), paginationFilter);
        }
        
        private string CreateUrl(string requestPath, string version)
        {
            if (string.IsNullOrEmpty(version))
                version = _apiEndpointOptions.DefaultVersion;

            return $"{_apiEndpointOptions.ApiUrl}/api/v{version}/companies{requestPath ?? string.Empty}";
        }
    }
}