using Career.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;

namespace Career.Migration.DataSeeder
{
    public abstract class DataSeederBase<TEntity> : IDataSeeder
        where TEntity : class, new()
    {
        private const string ConfigSectionName = "DataSeedFiles";
        
        protected readonly IRepository<TEntity> _repository;
        protected readonly ILogger<DataSeederBase<TEntity>> _logger;
        private readonly AsyncPolicy _asyncRetryPolicy;
        private readonly IConfiguration Configuration;
        private readonly IHostEnvironment _environment;

        protected DataSeederBase(
            IRepository<TEntity> repository,
            IHostEnvironment environment,
            IConfiguration configuration,
            ILogger<DataSeederBase<TEntity>> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _asyncRetryPolicy = GetAsyncPolicy();
        }

        public virtual async Task SeedDataAsync()
        {
            try
            {
                var collectionName = typeof(TEntity).Name;
                var jsonDataFile = GetDataFilePathFromConfig(collectionName);
                
                await _asyncRetryPolicy.ExecuteAsync(async () =>
                {
                    if (await _repository.AnyAsync())
                        return;

                    var data = await GetDataFromFile<IEnumerable<TEntity>>(jsonDataFile);
                    await _repository.AddRangeAsync(data);

                    _logger.LogInformation("Data seeder successful for {Entity}", typeof(TEntity).Name);
                });
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred in {Entity} data seeder. Exception Details :{Exception}", typeof(TEntity).Name, e);
            }
        }

        protected virtual async Task<T> GetDataFromFile<T>(string jsonFilePath)
        {
            if (!File.Exists(jsonFilePath))
                throw new FileNotFoundException("Database seed data is not found!", jsonFilePath);

            await using FileStream stream = new FileStream(jsonFilePath, FileMode.Open);
            return await JsonSerializer.DeserializeAsync<T>(stream);
        }
        
        private string GetDataFilePathFromConfig(string seedDataFileKey)
        {
            if(string.IsNullOrEmpty(seedDataFileKey))
                throw new ArgumentNullException(nameof(seedDataFileKey));

            var fullCongigKey = $"{ConfigSectionName}:{seedDataFileKey}";
            var rootPath = _environment.ContentRootPath;
            var filePath = Configuration[fullCongigKey];
            if (string.IsNullOrEmpty(filePath))
                throw new KeyNotFoundException($"Key {fullCongigKey} not found in the settings file.");

            return Path.Combine(rootPath, filePath);
        }

        private AsyncPolicy GetAsyncPolicy()
        {
            return Policy.Handle<TimeoutException>()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(4),
                    TimeSpan.FromSeconds(8)
                }, (exception, _ , retryCount, context) =>
                {
                    _logger.LogError($"Retry {retryCount} of {context.PolicyKey}. Exception Details: {exception}.");
                })
                .WithPolicyKey(typeof(TEntity).Name);
        }
    }
}