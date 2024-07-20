using DataAccessLayer.Helpers;
using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.DTO;
using ProductWebAPI.Models;

namespace ProductWebAPI.Interface
{
    public interface IProductRepository
    {
        public Task<ActionResult<List<Product>>> GetProducts(QueryObject query);
        public Task<ActionResult<Product>> GetProductById(int id);
        public Task<ActionResult<Product>> EditProduct(Product productChanges);
        public Task<ActionResult<Product>> CreateProduct(Product NewProduct);
        public Task<ActionResult<Product>> DeleteProduct(Product ProductForDeletion);

    }
}
