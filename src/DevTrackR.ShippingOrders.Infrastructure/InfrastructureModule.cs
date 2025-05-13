using DevTrackR.ShippingOrders.Core.Entities;
using DevTrackR.ShippingOrders.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace DevTrackR.ShippingOrders.Infrastructure
{
    public static class InfraestrucutureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services
                .AddMongo()
                .AddRepositories();

            return services;
        }

        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            services.AddSingleton<MongoDbOptions>(sp =>
            {
                var configuration = sp.GetService<IConfiguration>() ?? throw new InvalidOperationException("IConfiguration service is not registered.");
                var options = new MongoDbOptions();
                configuration.GetSection("Mongo").Bind(options);
                return options;
            });

            services.AddSingleton<IMongoClient>(sp =>
            {
                var options = sp.GetService<MongoDbOptions>() ?? throw new InvalidOperationException("MongoDbOptions service is not registered.");
                var client = new MongoClient(options.ConnectionString);
                var db = client.GetDatabase(options.Database);
                var dbSeed = new DbSeed(db);
                dbSeed.Populate();

                return client;
            });

            services.AddTransient<IMongoDatabase>(sp =>
            {
                BsonSerializer.RegisterSerializationProvider(new GuidSerializationProvider());
                var options = sp.GetService<MongoDbOptions>() ?? throw new InvalidOperationException("MongoDbOptions service is not registered.");
                var client = sp.GetService<IMongoClient>() ?? throw new InvalidOperationException("IMongoClient service is not registered.");
                return client.GetDatabase(options.Database);
            });

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IShippingOrderRepository, ShippingOrderRepository>();
            services.AddScoped<IShippingServiceRepository, ShippingServiceRepository>();
            return services;
        }

        private class GuidSerializationProvider : IBsonSerializationProvider
        {
            public IBsonSerializer? GetSerializer(Type type)
            {
                if (type == typeof(Guid) || type == typeof(Guid?)) return new GuidSerializer(GuidRepresentation.Standard);
                return null;
            }
        }
    }
}
