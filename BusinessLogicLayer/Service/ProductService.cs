using BusinessLogicLayer.Interface;
using BusinessLogicLayer.Service_Interfaces;
using DataAccessLayer.Helpers;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.Data;
using ProductWebAPI.DTO;
using ProductWebAPI.Interface;
using ProductWebAPI.Mapper;
using ProductWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly IProductRepository _productRepo;
        private readonly IUserProductService _userProductService;
        private DataContext context;
        private IProductRepository @object;
        private IProductRepository object1;
        private IProductService object2;

        public ProductService(DataContext context, IProductRepository @object)
        {
            this.context = context;
            this.@object = @object;
        }

        public ProductService(IProductRepository object1, IProductService object2)
        {
            this.object1 = object1;
            this.object2 = object2;
        }

        public ProductService(DataContext context, IProductRepository productRepo, IUserProductService userProductService)
        {
            _context = context;
            _productRepo = productRepo;
            _userProductService = userProductService;
        }

        public async Task<ActionResult<UserProduct>> CreateProductAsync(ProductDTO productDTO, string userId)
        {
            if (_productRepo.ProductExists(productDTO.Name))
            {
                return new ConflictObjectResult("Product with the same name already exists");
            }

            var product = new Product
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                OwnerId = userId
            };

            var createdProduct = await _productRepo.CreateProduct(product);
            return await _userProductService.CreateUserProductAsync(createdProduct.Value.Id, userId);
        }

        public async Task<ActionResult<Product?>> DeleteProduct(int productId, string userId)
        {
            var product = _context.Products.Where(p => p.Id == productId).FirstOrDefault();
            if (product == null)
                return null;

            if (product.OwnerId == userId)
            {
                var data = _context.UserProducts.Where(up => up.ProductId == productId).ToList();
                foreach (var productDel in data)
                {
                    await _userProductService.DeleteUserProductAsync(productDel);
                }
                return await _productRepo.DeleteProduct(product);
            }

            return null;
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
