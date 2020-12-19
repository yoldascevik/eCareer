using System;
using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http;
using Company.Application.Company.Commands.CreateCompany;
using Company.Application.Company.Dtos;
using Company.Application.Company.Queries.GetCompanies;

namespace Company.HttpClient.Company
{
    public interface ICompanyHttpClient : ICareerHttpClient
    {
        /// <summary>
        /// Get all companies
        /// </summary>
        /// <param name="query">Request params</param>
        /// <param name="version">Api version. Default = 1</param>
        /// <returns>Company list with pagination</returns>
        Task<ConsistentApiResponse<PagedList<CompanyDto>>> Get(GetCompaniesQuery query, string version = null);

        /// <summary>
        /// Get specific company by id
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="version">Api version. Default = 1</param>
        /// <returns>Company</returns>
        Task<ConsistentApiResponse<CompanyDto>> GetById(Guid companyId, string version = null);
        
        /// <summary>
        /// Get company address by company id
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="version">Api version. Default = 1</param>
        /// <returns>Company address info</returns>
        Task<ConsistentApiResponse<AddressDto>> GetCompanyAddress(Guid companyId, string version = null);
        
        /// <summary>
        /// Get company details by company id
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="version">Api version. Default = 1</param>
        /// <returns>Company details</returns>
        Task<ConsistentApiResponse<CompanyDetailDto>> GetCompanyDetails(Guid companyId, string version = null);
        
        /// <summary>
        /// Get company tax info
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="version">Api version. Default = 1</param>
        /// <returns>Company tax info</returns>
        Task<ConsistentApiResponse<TaxDto>> GetCompanyTaxInfo(Guid companyId, string version = null);
        
        /// <summary>
        /// Create new company
        /// </summary>
        /// <param name="command">Company Info</param>
        /// <param name="version">Api version. Default = 1</param>
        /// <returns>Created company id</returns>
        Task<ConsistentApiResponse<Guid>> Create(CreateCompanyCommand command, string version = null);

        /// <summary>
        /// Update company tax info
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="taxInfo">Company tax info</param>
        /// <param name="version">Api version. Default = 1</param>
        /// <returns>Company tax info</returns>
        Task<ConsistentApiResponse<TaxDto>> UpdateTaxInfo(Guid companyId, TaxDto taxInfo, string version = null);
        
        /// <summary>
        /// Update company address
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="address">Company address</param>
        /// <param name="version">Api version. Default = 1</param>
        /// <returns>Company address info</returns>
        Task<ConsistentApiResponse<AddressDto>> UpdateAddress(Guid companyId, AddressDto address, string version = null);
        
        /// <summary>
        /// Update company details
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="detail">Company info</param>
        /// <param name="version">Api version. Default = 1</param>
        /// <returns>Company detail info</returns>
        Task<ConsistentApiResponse<CompanyDetailDto>> UpdateDetail(Guid companyId, CompanyDetailDto detail, string version = null);

        /// <summary>
        /// Update company email
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="emailAddress">Company email address</param>
        /// <param name="version">Api version. Default = 1</param>
        Task<ConsistentApiResponse> UpdateEmailAddress(Guid companyId, string emailAddress, string version = null);
        
        /// <summary>
        /// Update company name
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="companyName">Company name</param>
        /// <param name="version">Api version. Default = 1</param>
        Task<ConsistentApiResponse> UpdateCompanyName(Guid companyId, string companyName, string version = null);
        
        /// <summary>
        /// Delete company
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="version">Api version. Default = 1</param>
        Task<ConsistentApiResponse> DeleteCompany(Guid companyId, string version = null);

        /// <summary>
        /// Get followers of company
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="paginationFilter">Pagination configuration</param>
        /// <param name="version">Api version. Default = 1</param>
        Task<ConsistentApiResponse<PagedList<Guid>>> GetCompanyFollowers(Guid companyId, PaginationFilter paginationFilter, string version = null);
    }
}