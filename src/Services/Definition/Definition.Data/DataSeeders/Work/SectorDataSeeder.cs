using Career.Migration.DataSeeder;
using Career.Mongo.Repository.Contracts;
using Definition.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Definition.Data.DataSeeders.Work
{
    public class SectorDataSeeder : DataSeederBase<Sector>
    {
        public SectorDataSeeder(
            IMongoRepository<Sector> repository, 
            IHostEnvironment environment, 
            IConfiguration configuration, 
            ILogger<DataSeederBase<Sector>> logger) 
            : base(repository, environment, configuration, logger)
        {
        }
    }
}