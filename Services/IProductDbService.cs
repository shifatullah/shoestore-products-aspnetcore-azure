using ShoeStore.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeStore.Products.Services
{

    public interface IProductDbService
    {
        Task<IEnumerable<Product>> GetItemsAsync(string query);
        Task<Product> GetItemAsync(string id);
        Task AddItemAsync(Product item);
        Task UpdateItemAsync(string id, Product item);
        Task DeleteItemAsync(string id);
    }
}