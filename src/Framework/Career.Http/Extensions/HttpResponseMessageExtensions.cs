using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Career.Http.Extensions
{
    internal static class HttpResponseMessageExtensions
    {
        internal static async Task<TResponse> DeSerializeResponseAsync<TResponse>(this HttpResponseMessage httpResponseMessage)
        {
            string resultContent = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(resultContent);
        }
    }
}