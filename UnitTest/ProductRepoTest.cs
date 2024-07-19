/*using Xunit;
using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Data;
using ProductWebAPI.Models;
using ProductWebAPI.Repository;
using ProductWebAPI.Interface;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using DataAccessLayer.Helpers;

namespace ProductWebAPI.Tests
{
    public class ProductRepositoryTests
    {
        private readonly DataContext _context;
        private readonly ProductRepository _repository;

        public ProductRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "ProductTestDb")
                .Options;

            _context = new DataContext(options);
            _repository = new ProductRepository(_context);

            // Seed data
            if (!_context.Products.Any())
            {
                _context.Products.AddRange(new List<Product>
                {
                    new Product { Id = 1, Name = "Product1", Description = "Description1", Price = 10 },
                    new Product { Id = 2, Name = "Product2", Description = "Description2", Price = 20 },
                });

                _context.SaveChanges();
            }
        }

        [Fact]
        public async Task CreateProduct_AddsProductToContext()
        {
            var product = new Product { Name = "Test Product", Description = "Description", Price = 10 };

            var result = await _repository.CreateProduct(product);

            Assert.Equal(product, result.Value);
            Assert.Equal(2, _context.Products.Count());
        }

        [Fact]
        public async Task CreateProduct_DoesntAddProductToContext()
        {
            var product = new Product { Name = "", Description = "", Price = 0 };

            var result = await _repository.CreateProduct(product);

            Assert.Equal(product, result.Value);
            Assert.Equal(2, _context.Products.Count());
        }

        [Fact]
        public async Task DeleteProduct_RemovesProductFromContext()
        {
            var product = await _context.Products.FindAsync(1);

            var result = await _repository.DeleteProduct(product);

            Assert.Equal(product, result.Value);
            Assert.Equal(1, _context.Products.Count());
        }

        [Fact]
        public async Task EditProduct_UpdatesProductInContext()
        {
            var updatedProduct = new Product { Id = 1, Name = "Updated Product", Description = "Updated Description", Price = 20 };

            var result = await _repository.EditProduct(1, updatedProduct);

            var product = await _context.Products.FindAsync(1);

            Assert.Equal("Updated Product", product.Name);
            Assert.Equal("Updated Description", product.Description);
            Assert.Equal(20, product.Price);
        }

        [Fact]
        public async Task GetProductById_ReturnsProductFromContext()
        {
            // Ensure context is empty or has unique keys
            _context.Products.RemoveRange(_context.Products);
            await _context.SaveChangesAsync();

            // Add a product to the context
            var product = new Product
            {
                Id = 1,
                Name = "Product1",
                Description = "Description1",
                Price = 10
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Retrieve the product by ID
            var result = await _repository.GetProductById(1);

            // Assert that the retrieved product matches the expected values
            Assert.NotNull(result);
            Assert.Equal(1, result.Value.Id);
            Assert.Equal("Product1", result.Value.Name);
        }

        [Fact]
        public async Task GetProductById_DoesntReturnProductFromContext()
        { 

            // Retrieve the product by ID
            var result = await _repository.GetProductById(int.MaxValue);

            // Assert that the retrieved product matches the expected values
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task GetProducts_ReturnsFilteredProductsFromContext()
        {
            var query = new QueryObject { Name = "Product1" };
            var result = await _repository.GetProducts(query);

            Assert.Single(result.Value);
            Assert.Equal("Product1", result.Value[0].Name);
        }

        public async Task GetProducts_DoesntReturnFilteredProductsFromContext()
        {
            var query = new QueryObject { Name = "qiojfoiqwjfoij12e" };
            var result = await _repository.GetProducts(query);

            Assert.Null(result);
        }
    }
}

**/
