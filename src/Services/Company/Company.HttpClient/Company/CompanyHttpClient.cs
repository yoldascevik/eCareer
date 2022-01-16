using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http.HttpClient;
using Company.Application.Company.Commands.CreateCompany;
using Company.Application.Company.Dtos;
using Company.Application.Company.Queries.GetCompanies;
using Microsoft.AspNetCore.Http;

namespace Company.HttpClient.Company;

public class CompanyHttpClient : CareerHttpClient, ICompanyHttpClient
{
    public CompanyHttpClient(System.Net.Http.HttpClient httpClient, IHttpContextAccessor httpContext)
        : base(httpClient, httpContext)
    {
    }

    // GET api/companies
    public async Task<ConsistentApiResponse<PagedList<CompanyDto>>> Get(GetCompaniesQuery query)
    {
        return await GetAsync<ConsistentApiResponse<PagedList<CompanyDto>>>(string.Empty, query);
    }

    // GET api/companies/{id}
    public async Task<ConsistentApiResponse<CompanyDto>> GetById(Guid companyId)
    {
        return await GetAsync<ConsistentApiResponse<CompanyDto>>(string.Empty, companyId);
    }

    // GET api/companies/{id}/address
    public async Task<ConsistentApiResponse<AddressDto>> GetCompanyAddress(Guid companyId)
    {
        return await GetAsync<ConsistentApiResponse<AddressDto>>($"{companyId}/address");
    }

    // GET api/companies/{id}/detail
    public async Task<ConsistentApiResponse<CompanyDetailDto>> GetCompanyDetails(Guid companyId)
    {
        return await GetAsync<ConsistentApiResponse<CompanyDetailDto>>($"{companyId}/detail");
    }

    // GET api/companies/{id}/tax
    public async Task<ConsistentApiResponse<TaxDto>> GetCompanyTaxInfo(Guid companyId)
    {
        return await GetAsync<ConsistentApiResponse<TaxDto>>($"{companyId}/tax");
    }

    // POST api/companies
    public async Task<ConsistentApiResponse<object>> Create(CreateCompanyCommand command)
    {
        return await PostAsync<ConsistentApiResponse<object>>(string.Empty, command);
    }

    // PUT api/companies/{id}/tax
    public async Task<ConsistentApiResponse<TaxDto>> UpdateTaxInfo(Guid companyId, TaxDto taxInfo)
    {
        return await PutAsync<ConsistentApiResponse<TaxDto>>($"{companyId}/tax", taxInfo);
    }

    // PUT api/companies/{id}/address
    public async Task<ConsistentApiResponse<AddressDto>> UpdateAddress(Guid companyId, AddressDto address)
    {
        return await PutAsync<ConsistentApiResponse<AddressDto>>($"{companyId}/address", address);
    }

    // PUT api/companies/{id}/detail
    public async Task<ConsistentApiResponse<CompanyDetailDto>> UpdateDetail(Guid companyId, CompanyDetailDto detail)
    {
        return await PutAsync<ConsistentApiResponse<CompanyDetailDto>>($"{companyId}/detail", detail);
    }

    // PUT api/companies/{id}/email
    public async Task<ConsistentApiResponse> UpdateEmailAddress(Guid companyId, string emailAddress)
    {
        return await PutAsync<ConsistentApiResponse>($"{companyId}/email", emailAddress);
    }

    // PUT api/companies/{id}/name
    public async Task<ConsistentApiResponse> UpdateCompanyName(Guid companyId, string companyName)
    {
        return await PutAsync<ConsistentApiResponse>($"{companyId}/name", companyName);
    }

    // DELETE api/companies/{id}
    public async Task<ConsistentApiResponse> DeleteCompany(Guid companyId)
    {
        return await DeleteAsync<ConsistentApiResponse>($"{companyId}");
    }

    // GET api/companies/{id}/followers
    public async Task<ConsistentApiResponse<PagedList<Guid>>> GetCompanyFollowers(Guid companyId, PaginationFilter paginationFilter)
    {
        return await GetAsync<ConsistentApiResponse<PagedList<Guid>>>($"{companyId}/followers", paginationFilter);
    }
}