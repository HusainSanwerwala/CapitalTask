using Microsoft.Azure.Cosmos;

namespace CapitalTask.Services
{
    // CosmosDbService.cs

    public class CosmosService : ICosmosService
    {
        private readonly IConfiguration _config;
        private Database _database;
        private readonly ILogger<CosmosService> _logger;

        public CosmosService(IConfiguration config, ILogger<CosmosService> logger)
        {
            _config = config;
            var client = new CosmosClient(
                connectionString: _config.GetConnectionString("ProductionConn")
            );
            _database = client.GetDatabase(_config["AzureCosmos:DatabaseName"]);
            _logger = logger;
        }

        public async Task AddItemAsync<T>(T item, string containerName)
        {
            _logger.LogInformation("before get container");
            var _container = _database.GetContainer(containerName);
            _logger.LogInformation("after get container");
            await _container.CreateItemAsync(item);
        }

        public async Task<T> GetItemAsync<T>(string id, string containerName)
        {
            var _container = _database.GetContainer(containerName);

            string sqlQuery = $"SELECT * FROM c WHERE c.id = '{id}'";
            var query = _container.GetItemQueryIterator<T>(new QueryDefinition(sqlQuery));
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                return response.FirstOrDefault();
            }
            return default(T);
        }

        public async Task<IEnumerable<T>> GetItemsAsync<T>(string queryString, string containerName)
        {
            var _container = _database.GetContainer(containerName);
            var query = _container.GetItemQueryIterator<T>(new QueryDefinition(queryString));
            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task UpdateItemAsync<T>(string id, T item, string containerName)
        {
            var _container = _database.GetContainer(containerName);
            await _container.ReplaceItemAsync(item, id, new PartitionKey(id));
        }

        public async Task DeleteItemAsync(string id, string containerName)
        {
            var _container = _database.GetContainer(containerName);
            await _container.DeleteItemAsync<object>(id, new PartitionKey(id));
        }
    }
}