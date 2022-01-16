using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Career.Http.Middleware;

public class HttpClientRequestIdDelegatingHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (!request.Headers.Contains("x-requestid"))
            request.Headers.Add("x-requestid", Guid.NewGuid().ToString());

        return await base.SendAsync(request, cancellationToken);
    }
}