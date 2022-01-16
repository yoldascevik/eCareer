using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Career.Http.Middleware;

public class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccesor;

    public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccesor)
    {
        _httpContextAccesor = httpContextAccesor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        StringValues authorizationHeader = _httpContextAccesor.HttpContext.Request.Headers["Authorization"];

        if (!string.IsNullOrEmpty(authorizationHeader))
            request.Headers.Add("Authorization", new string[] { authorizationHeader });
            
        // TODO
        // var token =  GetToken();
        // if (token != null)
        //     request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken);
    }
}