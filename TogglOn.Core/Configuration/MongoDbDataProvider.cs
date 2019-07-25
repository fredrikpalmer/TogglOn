using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using TogglOn.Client.Abstractions.Command;
using TogglOn.Client.Abstractions.Query;
using TogglOn.Contract.Commands;
using TogglOn.Contract.Models;
using TogglOn.Contract.Queries;
using TogglOn.Core.CommandHandlers;
using TogglOn.Core.QueryHandlers;
using TogglOn.Core.Repositories;
using TogglOn.DependencyInjection.Abstractions;

namespace TogglOn.Core.Configuration
{
    internal class MongoDbDataProvider : IDataProvider
    {
        private readonly string _connectionString;

        public MongoDbDataProvider(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void Configure(IServiceConfigurator configurator)
        {
            ConfigureDb();
            ConfigureIoC(configurator);
        }

        private static void ConfigureDb()
        {
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, type => true);

            BsonClassMap.RegisterClassMap<NamespaceDto>();
            BsonClassMap.RegisterClassMap<EnvironmentDto>();
            BsonClassMap.RegisterClassMap<FeatureGroupDto>();

            BsonClassMap.RegisterClassMap<FeatureToggleDto>();
            BsonClassMap.RegisterClassMap<FeatureToggleRuleDto>();

            BsonSerializer.RegisterIdGenerator(
                typeof(Guid),
                GuidGenerator.Instance
            );
        }

        private void ConfigureIoC(IServiceConfigurator configurator)
        {
            configurator.AddSingleton(typeof(IMongoClient), IServiceProvider => new MongoClient(_connectionString));
            configurator.AddSingleton<IInitializeDbRepository, MongoDbInitializeDbRepository>();
            configurator.AddSingleton<INamespaceRepository, MongoDbNamespaceRepository>();
            configurator.AddSingleton<IEnvironmentRepository, MongoDbEnvironmentRepository>();
            configurator.AddSingleton<IFeatureGroupRepository, MongoDbFeatureGroupRepository>();
            configurator.AddSingleton<IFeatureToggleRepository, MongoDbFeatureToggleRepository>();

            configurator.AddSingleton<ICommandHandler<InitializeTogglOnCommand, VoidResult>, InitializeTogglOnCommandHandler>();
            configurator.AddSingleton<ICommandHandler<IncrementUsageStatisticsCommand, VoidResult>, IncrementUsageStatisticsCommandHandler>();

            configurator.AddSingleton<IQueryHandler<GetAllFeatureTogglesQuery, IList<FeatureToggleDto>>, GetAllFeatureTogglesQueryHandler>();
            configurator.AddSingleton<IQueryHandler<GetAllFeatureGroupsQuery, IList<FeatureGroupDto>>, GetAllFeatureGroupsQueryHandler>();
        }
    }
}
