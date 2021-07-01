using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;
using ShoeStore.Products.Entities;
using ShoeStore.Products.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeStore.Products.Controllers
{
    [Authorize]
    [RequiredScope("access_as_user")] // The web API will only accept tokens 1) for users, and 2) having the "access_as_user" scope for this API
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductDbService _productDbService;

        public ProductsController(
            ILogger<ProductsController> logger,
            IProductDbService productDbService)
        {
            _logger = logger;
            _productDbService = productDbService;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {            
            return await _productDbService.GetItemsAsync("SELECT * FROM c");            
        }

        [HttpPost]
        public async Task<Product> Post(Product model)
        {
            Product product = null;
            if (!string.IsNullOrWhiteSpace(model.Id))
                product = (await _productDbService.GetItemAsync(model.Id.ToString()));

            if (product == null)
            {
                product = new Product();
                product.Id = Guid.NewGuid().ToString();
                product.Name = model.Name;
                await _productDbService.AddItemAsync(product);
            }
            else
            {
                product.Name = model.Name;
                await _productDbService.UpdateItemAsync(product.Id, product);
            }

            return product;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            Product product = (await _productDbService.GetItemAsync(id));

            if (product != null)
            {
                await _productDbService.DeleteItemAsync(id);
            }

            return Ok();
        }
    }
}
