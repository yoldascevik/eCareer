using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Career.Http.Extensions;
using Microsoft.AspNetCore.Http;

namespace Career.Http
{
    public abstract class CareerHttpClient: ICareerHttpClient
    {
        protected readonly HttpClient HttpClient;
        protected readonly IHttpContextAccessor HttpContext;
        
        protected CareerHttpClient(HttpClient httpClient, IHttpContextAccessor httpContext)
        {
            HttpClient = httpClient;
            HttpContext = httpContext;
        }
        
        public virtual async Task<TResponse> GetAsync<TResponse>(
            string urlPath, 
            object @params, 
            Dictionary<string, string> requestHeaders = null, 
            CancellationToken cancellationToken = default(CancellationToken)) 
            where TResponse : class
        {
            AppendCustomHttpHeaders(requestHeaders);
            urlPath = UrlExtensions.GetUrlWithQueryObject(urlPath, @params);
            
            var responseHttpMessage = await HttpClient.GetAsync(urlPath, cancellationToken);
            return await responseHttpMessage.DeSerializeResponseAsync<TResponse>();
        }

        public virtual async Task<TResponse> GetAsync<TResponse>(
            string urlPath, 
            Dictionary<string, string> requestHeaders = null, 
            CancellationToken cancellationToken = default(CancellationToken)) 
            where TResponse : class
        {
            AppendCustomHttpHeaders(requestHeaders);

            var responseHttpMessage = await HttpClient.GetAsync(urlPath, cancellationToken);
            return await responseHttpMessage.DeSerializeResponseAsync<TResponse>();
        }

        public virtual async Task<TResponse> PostAsync<TResponse>(
            string urlPath, 
            object @object, 
            Dictionary<string, string> requestHeaders = null, 
            CancellationToken cancellationToken = default(CancellationToken)) 
            where TResponse : class
        {
            AppendCustomHttpHeaders(requestHeaders);

            StringContent stringContent = ConvertToStringContent(JsonSerializer.Serialize(@object));
            var responseHttpMessage = await HttpClient.PostAsync(urlPath, stringContent, cancellationToken);
            
            return await responseHttpMessage.DeSerializeResponseAsync<TResponse>();
        }

        public virtual async Task<TResponse> PutAsync<TResponse>(
            string urlPath, 
            object @object, 
            Dictionary<string, string> requestHeaders = null, 
            CancellationToken cancellationToken = default(CancellationToken)) 
            where TResponse : class
        {
            AppendCustomHttpHeaders(requestHeaders);

            StringContent stringContent = ConvertToStringContent(JsonSerializer.Serialize(@object));
            var responseHttpMessage = await HttpClient.PutAsync(urlPath, stringContent, cancellationToken);
            
            return await responseHttpMessage.DeSerializeResponseAsync<TResponse>();
        }

        public virtual async Task<TResponse> DeleteAsync<TResponse>(
            string urlPath, 
            object @object, 
            Dictionary<string, string> requestHeaders = null, 
            CancellationToken cancellationToken = default(CancellationToken)) 
            where TResponse : class
        {
            AppendCustomHttpHeaders(requestHeaders);
            
            var responseHttpMessage = await HttpClient.DeleteAsync(urlPath, cancellationToken);
            return await responseHttpMessage.DeSerializeResponseAsync<TResponse>();
        }
        
        protected StringContent ConvertToStringContent(string strContent, string contentType = "application/json")
        {
            return new StringContent(strContent, Encoding.UTF8, contentType);
        }

        protected void AppendCustomHttpHeaders(Dictionary<string, string> headers)
        {
            if (headers == null || !headers.Any())
                return;

            foreach (KeyValuePair<string,string> header in headers)
                HttpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        }
    }
}