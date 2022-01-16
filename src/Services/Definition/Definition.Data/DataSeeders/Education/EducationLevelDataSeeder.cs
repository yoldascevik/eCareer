using Career.Migration.DataSeeder;
using Career.Mongo.Repository.Contracts;
using Definition.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Definition.Data.DataSeeders.Education;

public class EducationLevelDataSeeder : DataSeederBase<EducationLevel>
{
    public EducationLevelDataSeeder(
        IMongoRepository<EducationLevel> repository, 
        IHostEnvironment environment, 
        IConfiguration configuration, 
        ILogger<EducationLevelDataSeeder> logger) 
        : base(repository, environment, configuration, logger)
    {
    }
}