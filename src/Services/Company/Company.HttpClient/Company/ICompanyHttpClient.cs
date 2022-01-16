using System;
using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http.HttpClient;
using Company.Application.Company.Commands.CreateCompany;
using Company.Application.Company.Dtos;
using Company.Application.Company.Queries.GetCompanies;

namespace Company.HttpClient.Company;

public interface ICompanyHttpClient : ICareerHttpClient
{
    /// <summary>
    /// Get all companies
    /// </summary>
    /// <param name="query">Request params</param>
    /// <returns>Company list with pagination</returns>
    Task<ConsistentApiResponse<PagedList<CompanyDto>>> Get(GetCompaniesQuery query);

    /// <summary>
    /// Get specific company by id
    /// </summary>
    /// <param name="companyId">Company Id</param>
    /// <returns>Company</returns>
    Task<ConsistentApiResponse<CompanyDto>> GetById(Guid companyId);
        
    /// <summary>
    /// Get company address by company id
    /// </summary>
    /// <param name="companyId">Company Id</param>
    /// <returns>Company address info</returns>
    Task<ConsistentApiResponse<AddressDto>> GetCompanyAddress(Guid companyId);
        
    /// <summary>
    /// Get company details by company id
    /// </summary>
    /// <param name="companyId">Company Id</param>
    /// <returns>Company details</returns>
    Task<ConsistentApiResponse<CompanyDetailDto>> GetCompanyDetails(Guid companyId);
        
    /// <summary>
    /// Get company tax info
    /// </summary>
    /// <param name="companyId">Company Id</param>
    /// <returns>Company tax info</returns>
    Task<ConsistentApiResponse<TaxDto>> GetCompanyTaxInfo(Guid companyId);

    /// <summary>
    /// Create new company
    /// </summary>
    /// <param name="command">Company Info</param>
    /// <returns>Created company id</returns>
    Task<ConsistentApiResponse<object>> Create(CreateCompanyCommand command);

    /// <summary>
    /// Update company tax info
    /// </summary>
    /// <param name="companyId">Company Id</param>
    /// <param name="taxInfo">Company tax info</param>
    /// <returns>Company tax info</returns>
    Task<ConsistentApiResponse<TaxDto>> UpdateTaxInfo(Guid companyId, TaxDto taxInfo);
        
    /// <summary>
    /// Update company address
    /// </summary>
    /// <param name="companyId">Company Id</param>
    /// <param name="address">Company address</param>
    /// <returns>Company address info</returns>
    Task<ConsistentApiResponse<AddressDto>> UpdateAddress(Guid companyId, AddressDto address);
        
    /// <summary>
    /// Update company details
    /// </summary>
    /// <param name="companyId">Company Id</param>
    /// <param name="detail">Company info</param>
    /// <returns>Company detail info</returns>
    Task<ConsistentApiResponse<CompanyDetailDto>> UpdateDetail(Guid companyId, CompanyDetailDto detail);

    /// <summary>
    /// Update company email
    /// </summary>
    /// <param name="companyId">Company Id</param>
    /// <param name="emailAddress">Company email address</param>
    Task<ConsistentApiResponse> UpdateEmailAddress(Guid companyId, string emailAddress);
        
    /// <summary>
    /// Update company name
    /// </summary>
    /// <param name="companyId">Company Id</param>
    /// <param name="companyName">Company name</param>
    Task<ConsistentApiResponse> UpdateCompanyName(Guid companyId, string companyName);
        
    /// <summary>
    /// Delete company
    /// </summary>
    /// <param name="companyId">Company Id</param>
    Task<ConsistentApiResponse> DeleteCompany(Guid companyId);

    /// <summary>
    /// Get followers of company
    /// </summary>
    /// <param name="companyId">Company Id</param>
    /// <param name="paginationFilter">Pagination configuration</param>
    Task<ConsistentApiResponse<PagedList<Guid>>> GetCompanyFollowers(Guid companyId, PaginationFilter paginationFilter);
}