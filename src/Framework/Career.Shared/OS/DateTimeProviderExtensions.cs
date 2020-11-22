using Microsoft.Extensions.DependencyInjection;

namespace Career.Shared.OS
{
    public  static class DateTimeProviderExtensions
    {
        public static IServiceCollection AddDateTimeProvider(this IServiceCollection services)
            => services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    }
}