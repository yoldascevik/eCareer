using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Career.Mvc.Extensions
{
    public static class ApiVersionExtension
    {
        public static IServiceCollection AddApiVersion(this IServiceCollection services)
            => services.AddApiVersioning(v =>
            {
                v.DefaultApiVersion = new ApiVersion(1, 0);
                v.AssumeDefaultVersionWhenUnspecified = true;
                v.ReportApiVersions = true;
            });
    }
}
