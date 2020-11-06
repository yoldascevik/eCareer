using Career.Migration.DataSeeder;
using Career.Mongo.Repository.Contracts;
using Definition.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Definition.Data.DataSeeders.Location
{
    public class DistrictDataSeeder : DataSeederBase<District>
    {
        public DistrictDataSeeder(
            IMongoRepository<District> repository, 
            IHostEnvironment environment, 
            IConfiguration configuration, 
            ILogger<DataSeederBase<District>> logger) 
            : base(repository, environment, configuration, logger)
        {
        }
    }
}
