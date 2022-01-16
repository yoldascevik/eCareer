using Career.Migration.DataSeeder;
using Career.Mongo.Repository.Contracts;
using Definition.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Definition.Data.DataSeeders.Education;

public class EducationTypeDataSeeder : DataSeederBase<EducationType>
{
    public EducationTypeDataSeeder(
        IMongoRepository<EducationType> repository, 
        IHostEnvironment environment, 
        IConfiguration configuration, 
        ILogger<EducationTypeDataSeeder> logger) 
        : base(repository, environment, configuration, logger)
    {
    }
}