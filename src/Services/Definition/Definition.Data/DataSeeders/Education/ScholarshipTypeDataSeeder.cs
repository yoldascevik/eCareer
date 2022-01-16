using Career.Migration.DataSeeder;
using Career.Mongo.Repository.Contracts;
using Definition.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Definition.Data.DataSeeders.Education;

public class ScholarshipTypeDataSeeder : DataSeederBase<ScholarshipType>
{
    public ScholarshipTypeDataSeeder(
        IMongoRepository<ScholarshipType> repository, 
        IHostEnvironment environment, 
        IConfiguration configuration, 
        ILogger<ScholarshipTypeDataSeeder> logger) 
        : base(repository, environment, configuration, logger)
    {
    }
}