using Career.Domain.DomainEvent;
using Career.Domain.DomainEvent.Dispatcher;
using Career.Mongo.Context;
using Career.Mongo.Document;
using Career.Mongo.Repository;
using Career.Mongo.Repository.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Career.Mongo
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            services.AddSingleton(context =>
            {
                IConfiguration configuration = context.GetService<IConfiguration>();
                string connectionStrings = configuration["mongo:connectionString"];
                if (string.IsNullOrWhiteSpace(connectionStrings))
                    throw new MongoConfigurationException("Mongo connectionstring not found!");

                return new MongoClient(connectionStrings);
            });

            services.AddScoped(context =>
            {
                IConfiguration configuration = context.GetService<IConfiguration>();
                MongoClient client = context.GetService<MongoClient>();
                string databaseName = configuration["mongo:database"];
                if (string.IsNullOrWhiteSpace(databaseName))
                    throw new MongoConfigurationException("Mongo database name not found!");

                return client.GetDatabase(databaseName);
            });

            services.AddScoped(typeof(IMongoQueryRepository<>), typeof(MongoQueryRepository<>));
            services.AddScoped(typeof(IMongoCommandRepository<>), typeof(MongoCommandRepository<>));
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            return services;
        }

        public static IServiceCollection AddMongoContext<TContext>(this IServiceCollection services)
            where TContext : MongoContext
        {
            services.UseDomainEventDispatcherAttribute(typeof(TContext));
            return services.AddScoped<IMongoContext, TContext>();
        }


        public static IServiceCollection AddMongoRepository<TEntity>(this IServiceCollection services) 
            where TEntity: class, IDocument
        {
            services.AddScoped<IMongoRepository<TEntity>>( x =>
            {
                var mongoContext = x.GetService<IMongoContext>();
                var domainEventDispatcher = x.GetRequiredService<IDomainEventDispatcher>();
                return new MongoRepository<TEntity>(mongoContext, domainEventDispatcher);
            });

            return services;
        }
    }
}
