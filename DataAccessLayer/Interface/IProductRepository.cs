using DataAccessLayer.Helpers;
using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.Models;

namespace ProductWebAPI.Interface
{
    public interface IProductRepository
    {
        public Task<ActionResult<List<Product>>> GetProducts(QueryObject query);
        public Task<ActionResult<Product>> GetProductById(int id);
        public int GetProductCount();
        public decimal GetProductsAveragePrice();
        public Task<ActionResult<decimal>> GetProductsMinimumPrice();
        public Task<ActionResult<decimal>> GetProductMaximumPrice();
        public Task<ActionResult<Product>> EditProduct(Product productChanges);
        public Task<ActionResult<Product>> CreateProduct(Product NewProduct);
        public Task<ActionResult<Product>> DeleteProduct(Product ProductForDeletion);
        public bool ProductExists(string name);

    }
}
