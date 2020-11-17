using System;
using System.Linq;
using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Utilities.Pagination;
using Definition.Contract.Dto;
using Definition.HttpClient;
using Definition.HttpClient.Country;
using Definition.Test.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Definition.Test.Integration
{
    public class CountryHttpClientTest
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly bool _integrationTestDisabled;

        public CountryHttpClientTest()
        {
            IConfiguration configuration = ConfigurationHelper.GetConfiguration();
            var apiEndpointOptions = configuration.GetSection("ApiEndpoints:DefinitionApi").Get<ApiEndpointOptions>();
            _integrationTestDisabled = configuration.GetSection("DisableIntegrationTest").Get<bool>();

            IServiceCollection services = new ServiceCollection();
            services.AddDefinitionApiHttpClient(apiEndpointOptions);

            _serviceProvider = services.BuildServiceProvider();
        }

        [Theory]
        [InlineData(1,10)]
        [InlineData(2,5)]
        public async Task GetAsync_ShouldBeNotEmpty_WithPaging(int pageNumber, int pageSize)
        {
            // TODO: test disabled for github action CI pipeline.
            if (_integrationTestDisabled)
            {
                Assert.True(true);
                return;
            }
            
            // arrange
            var countryHttpClient = _serviceProvider.GetService<ICountryHttpClient>();
            if (countryHttpClient == null)
                throw new ArgumentNullException(nameof(countryHttpClient));
            
            var paginationFilter = new PaginationFilter(pageNumber, pageSize);

            // actual
            ConsistentApiResponse<PagedList<CountryDto>> response = await countryHttpClient.GetAsync(paginationFilter);

            // assert
            Assert.NotNull(response);
            Assert.Equal(pageNumber, response.Payload.PageNumber);
            Assert.Equal(pageSize, response.Payload.PageSize);
            Assert.True(response.Payload.Data.Any());
        }
    }
}