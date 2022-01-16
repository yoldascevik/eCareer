using Career.Migration.DataSeeder;
using Career.Mongo.Repository.Contracts;
using Definition.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Definition.Data.DataSeeders;

public class LanguageDataSeeder : DataSeederBase<Language>
{
    public LanguageDataSeeder(
        IMongoRepository<Language> repository, 
        IHostEnvironment environment, 
        IConfiguration configuration, 
        ILogger<LanguageDataSeeder> logger) 
        : base(repository, environment, configuration, logger)
    {
    }
}