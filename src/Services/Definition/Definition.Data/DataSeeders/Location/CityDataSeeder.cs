using Career.Migration.DataSeeder;
using Career.Mongo.Repository.Contracts;
using Definition.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Definition.Data.DataSeeders.Location
{
    public class CityDataSeeder : DataSeederBase<City>
    {
        public CityDataSeeder(
            IMongoRepository<City> repository, 
            IHostEnvironment environment, 
            IConfiguration configuration, 
            ILogger<CityDataSeeder> logger) 
            : base(repository, environment, configuration, logger)
        {
        }
    }
}