using MongoDB.Driver;
using System.Threading.Tasks;
using TogglOn.Core.Utils;
using System.Linq;
using System;
using Microsoft.Extensions.Logging;

namespace TogglOn.Core.Repositories
{
    internal class MongoDbInitializeDbRepository : IInitializeDbRepository
    {
        private readonly IMongoClient _mongoClient;
        private readonly ILogger<MongoDbInitializeDbRepository> _logger;

        public MongoDbInitializeDbRepository(IMongoClient mongoClient, ILogger<MongoDbInitializeDbRepository> logger)
        {
            _mongoClient = mongoClient;
            _logger = logger;
        }
        public async Task CreateAsync()
        {
            var cursor = await _mongoClient.ListDatabaseNamesAsync();
            var databaseNames = await cursor.ToListAsync();

            if(!databaseNames.Contains(DbObjects.Database, StringComparer.OrdinalIgnoreCase))
            {
                _logger.LogInformation("Initializing togglOn MongoDb");

                var database = _mongoClient.GetDatabase(DbObjects.Database);

                var createNamespaceCollectionTask = database.CreateCollectionAsync(DbObjects.Namespaces);
                var createEnvironmentCollectionTask = database.CreateCollectionAsync(DbObjects.Environments);
                var createFeatureGroupCollectionTask = database.CreateCollectionAsync(DbObjects.FeatureGroups);
                var createFeatureToggleCollectionTask = database.CreateCollectionAsync(DbObjects.FeatureToggles);

                await createNamespaceCollectionTask;
                await createEnvironmentCollectionTask;
                await createFeatureGroupCollectionTask;
                await createFeatureToggleCollectionTask;
            }
            else
            {
                _logger.LogInformation("Db already initialized");
            }
        }
    }
}
