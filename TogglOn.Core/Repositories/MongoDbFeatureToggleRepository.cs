using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TogglOn.Contract.Models;
using TogglOn.Core.Utils;

namespace TogglOn.Core.Repositories
{
    internal class MongoDbFeatureToggleRepository : IFeatureToggleRepository
    {
        private readonly IMongoClient _mongoClient;

        public MongoDbFeatureToggleRepository(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public async Task<IList<FeatureToggleDto>> GetAllAsync(string @namespace = null, string environment = null)
        {
            var collection = GetCollection();

            var filterBuilder = Builders<FeatureToggleDto>.Filter;

            if (string.IsNullOrEmpty(@namespace) && string.IsNullOrEmpty(environment))
                return await collection.Find(new BsonDocument()).ToListAsync();
            else
                return await collection.Find((filterBuilder.Eq(x => x.Namespace, @namespace) & filterBuilder.Eq(x => x.Environment, environment))).ToListAsync(); 
        }

        public async Task IncrementUsageStatisticsAsync(Guid id, bool enabled)
        {
            var collection = GetCollection();

            var filterBuilder = Builders<FeatureToggleDto>.Filter;
            var filter = filterBuilder.Eq(x => x.Id, id);

            var updateBuilder = Builders<FeatureToggleDto>.Update;

            var update = updateBuilder.Inc(x => x.TotalRequestAmount, 1);
            if (enabled)
            {
                update = update.Inc(x => x.EnabledRequestAmount, 1);
            }

            await collection.UpdateOneAsync(filter, update);
        }

        public Task InsertManyAsync(IList<FeatureToggleDto> featureToggles)
        {
            var collection = GetCollection();

            return collection.InsertManyAsync(featureToggles);
        }

        public async Task UpdateActivatedAsync(Guid id, bool activated)
        {
            var collection = GetCollection();

            var filterBuilder = Builders<FeatureToggleDto>.Filter;
            var filter = filterBuilder.Eq(x => x.Id, id);

            var updateBuilder = Builders<FeatureToggleDto>.Update;
            var update = updateBuilder.Set(x => x.Activated, activated);

            await collection.UpdateOneAsync(filter, update);
        }

        private IMongoCollection<FeatureToggleDto> GetCollection()
        {
            var database = GetDatabase();

            return database.GetCollection<FeatureToggleDto>(DbObjects.FeatureToggles);
        }

        private IMongoDatabase GetDatabase()
        {
            return _mongoClient.GetDatabase(DbObjects.Database);
        }
    }
}
