using BusinessLogicLayer.Interface;
using DataAccessLayer.Helpers;
using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.Data;
using ProductWebAPI.DTO;
using ProductWebAPI.Interface;
using ProductWebAPI.Mapper;
using ProductWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly IProductRepository _productRepo;

        public ProductService(DataContext context, IProductRepository productRepo)
        {
            _context = context;
            _productRepo = productRepo;
        }

        public async Task<ActionResult<Product>> CreateProductAsync(ProductDTO product)
        {
            return await _productRepo.CreateProduct(product);
        }

        public async Task<ActionResult<Product?>> DeleteProduct(int productId)
        {
            var product = _context.Products.Where(p => p.Id == productId).FirstOrDefault();
            if (product == null)
                return null;

            return await _productRepo.DeleteProduct(product);
        }

        public async Task<ActionResult<Product>> EditProductAsync(int idOfProductForChange, ProductDTO productChanges)
        {
            var productForChange = _context.Products.Where(p => p.Id == idOfProductForChange).FirstOrDefault();

            if (productForChange == null)
                return null;

            productForChange.Name = productChanges.Name;
            productForChange.Description = productChanges.Description;
            productForChange.Price = productChanges.Price;

            return await _productRepo.EditProduct(productForChange);
        }

        public async Task<ActionResult<Product>> GetProductByIdAsync(int productId)
        {
            return await _productRepo.GetProductById(productId);
        }

        public async Task<ActionResult<List<Product>>> GetProductsAsync(QueryObject query)
        {
            return await _productRepo.GetProducts(query);
        }
    }
}
