using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Career.Http.HttpClient;

public interface ICareerHttpClient
{
    Task<TResponse> GetAsync<TResponse>(string urlPath, object @params,
        Dictionary<string, string> requestHeaders = null,
        CancellationToken cancellationToken = default(CancellationToken)) where TResponse : class;

    Task<TResponse> GetAsync<TResponse>(string urlPath,
        Dictionary<string, string> requestHeaders = null,
        CancellationToken cancellationToken = default(CancellationToken)) where TResponse : class;
        
    Task<TResponse> PostAsync<TResponse>(string urlPath, object @object,
        Dictionary<string, string> requestHeaders = null,
        CancellationToken cancellationToken = default(CancellationToken)) where TResponse : class;

    Task<TResponse> PutAsync<TResponse>(string urlPath, object @object,
        Dictionary<string, string> requestHeaders = null,
        CancellationToken cancellationToken = default(CancellationToken)) where TResponse : class;

    Task<TResponse> DeleteAsync<TResponse>(string urlPath,
        Dictionary<string, string> requestHeaders = null,
        CancellationToken cancellationToken = default(CancellationToken)) where TResponse : class;

    void AppendCustomHttpHeader(string key, string value);
}