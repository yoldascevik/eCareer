using System;
using Microsoft.Extensions.DependencyInjection;

namespace Career.IoC
{
    public static class DIResolver
    {
        private static IServiceProvider _serviceProvider;

        public static IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceProvider == null)
                    throw new ArgumentException("ServiceProvider not set for DIResolver!");   

                return _serviceProvider;
            }
        }

        public static void SetProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public static IServiceScope CreateScope()
        {
            return ServiceProvider.CreateScope();
        }
    }
}