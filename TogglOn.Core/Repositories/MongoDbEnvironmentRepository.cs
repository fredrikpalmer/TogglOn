using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TogglOn.Contract.Models;
using TogglOn.Core.Utils;

namespace TogglOn.Core.Repositories
{
    internal class MongoDbEnvironmentRepository : IEnvironmentRepository
    {
        private readonly IMongoClient _mongoClient;

        public MongoDbEnvironmentRepository(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public async Task<IList<EnvironmentDto>> GetAllAsync()
        {
            var database = GetDatabase();
            var collection = database.GetCollection<EnvironmentDto>(DbObjects.Environments);
            
            return await collection.Find(new BsonDocument()).ToListAsync();
        }

        public Task InsertAsync(EnvironmentDto environment)
        {
            var database = GetDatabase();

            var collection = database.GetCollection<EnvironmentDto>(DbObjects.Environments);

            return collection.InsertOneAsync(environment);
        }

        private IMongoDatabase GetDatabase()
        {
            return _mongoClient.GetDatabase(DbObjects.Database);
        }
    }
}
