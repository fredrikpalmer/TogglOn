using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using TogglOn.Contract.Models;
using TogglOn.Core.Utils;

namespace TogglOn.Core.Repositories
{
    internal class MongoDbFeatureGroupRepository : IFeatureGroupRepository
    {
        private IMongoClient _mongoClient;

        public MongoDbFeatureGroupRepository(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public  async Task<IList<FeatureGroupDto>> GetAllAsync()
        {
            var database = GetDatabase();

            var collection = database.GetCollection<FeatureGroupDto>(DbObjects.FeatureGroups);

            return await collection.Find(new BsonDocument()).ToListAsync();
        }

        public Task InsertManyAsync(IList<FeatureGroupDto> featureGroups)
        {
            var database = GetDatabase();

            var collection = database.GetCollection<FeatureGroupDto>(DbObjects.FeatureGroups);

            return collection.InsertManyAsync(featureGroups);
        }

        private IMongoDatabase GetDatabase()
        {
            return _mongoClient.GetDatabase(DbObjects.Database);
        }
    }
}
