using Microsoft.Extensions.DependencyInjection;

namespace Career.Shared.System.DateTimeProvider
{
    public  static class DateTimeProviderExtensions
    {
        public static IServiceCollection AddDateTimeProvider(this IServiceCollection services)
            => services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    }
}