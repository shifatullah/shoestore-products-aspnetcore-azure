using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using ShoeStore.Products.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeStore.Products.Services
{
    public class ProductDbService : IProductDbService
    {
        private readonly Container _container;
        private readonly IConfiguration _configuration;
        private readonly string _containerName = "Product";

        public ProductDbService(
            CosmosClient client,
            IConfiguration configuration)
        {
            _configuration = configuration;

            string databaseName = _configuration.GetValue<string>("Products:Settings:Database");
            _containerName = _configuration.GetValue<string>("Products:Settings:Container");
            _container = client.GetContainer(databaseName, _containerName);

            DatabaseResponse database = client.CreateDatabaseIfNotExistsAsync(databaseName).Result;
            database.Database.CreateContainerIfNotExistsAsync(_containerName, "/id");
        }

        public async Task AddItemAsync(Product product)
        {
            await this._container.CreateItemAsync<Product>(product, new PartitionKey(product.Id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<Product>(id, new PartitionKey(id));
        }

        public async Task<Product> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Product> response = await this._container.ReadItemAsync<Product>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<Product>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Product>(new QueryDefinition(queryString));
            List<Product> results = new List<Product>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(string id, Product product)
        {
            await this._container.UpsertItemAsync<Product>(product, new PartitionKey(id));
        }
    }
}