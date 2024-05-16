namespace CapitalTask.Services
{
    public interface ICosmosService
    {
        public Task AddItemAsync<T>(T item, string containerName);

        public Task<T> GetItemAsync<T>(string id, string containerName);

        public Task<IEnumerable<T>> GetItemsAsync<T>(string queryString, string containerName);

        public Task UpdateItemAsync<T>(string id, T item, string containerName);

        public Task DeleteItemAsync(string id, string containerName);
    }
}