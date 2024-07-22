using DataAccessLayer.Helpers;
using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.DTO;
using ProductWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interface
{
    public interface IProductService
    {
        public Task<ActionResult<Product>> CreateProductAsync(ProductDTO product);
        public Task<ActionResult<List<Product>>> GetProductsAsync(QueryObject query);
        public Task<ActionResult<Product>> GetProductByIdAsync(int productId);
        public Task<ActionResult<Product>> EditProductAsync(int productForChange, ProductDTO productChanges);
        public Task<ActionResult<Product>> DeleteProduct(int productId);
        

    }
}
