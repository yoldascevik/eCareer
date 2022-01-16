using Career.Exceptions;

namespace Career.Http.HttpClient;

public static class HttpClientExtensions
{
    public static TClient WithVersion<TClient>(this TClient httpClient, string version, string versionHeaderKey = "X-Api-Version")
        where TClient : ICareerHttpClient
    {
        Check.NotNull(httpClient, nameof(httpClient));
        Check.NotNullOrEmpty(version, nameof(version));

        httpClient.AppendCustomHttpHeader(versionHeaderKey, version);

        return httpClient;
    }
}