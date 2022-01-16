using System.Text.Json;

namespace Career.Http.Extensions;

internal static class HttpResponseMessageExtensions
{
    internal static async Task<TResponse> DeSerializeResponseAsync<TResponse>(this HttpResponseMessage httpResponseMessage)
    {
        string resultContent = await httpResponseMessage.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TResponse>(resultContent, new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});
    }
}