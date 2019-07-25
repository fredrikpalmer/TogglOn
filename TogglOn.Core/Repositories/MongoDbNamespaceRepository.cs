using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using TogglOn.Contract.Models;
using TogglOn.Core.Utils;

namespace TogglOn.Core.Repositories
{
    internal class MongoDbNamespaceRepository : INamespaceRepository
    {
        private IMongoClient _mongoClient;

        public MongoDbNamespaceRepository(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public async Task<IList<NamespaceDto>> GetAllAsync()
        {
            var database = GetDatabase();

            var collection = database.GetCollection<NamespaceDto>(DbObjects.Namespaces);

            return await collection.Find(new BsonDocument()).ToListAsync();
        }

        public Task InsertAsync(NamespaceDto @namespace)
        {
            var database = GetDatabase();

            var collection = database.GetCollection<NamespaceDto>(DbObjects.Namespaces);

            return collection.InsertOneAsync(@namespace);
        }

        private IMongoDatabase GetDatabase()
        {
            return _mongoClient.GetDatabase(DbObjects.Database);
        }
    }
}
