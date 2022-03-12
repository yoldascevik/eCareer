namespace Gateway.Web.Api.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static RouteHandlerBuilder UseHealtCheckApiEndpoints(this IEndpointRouteBuilder builder)
    {
        return builder.MapGet("/api/HealthCheck", () => Results.Ok("Api is awake!"));
    }
}