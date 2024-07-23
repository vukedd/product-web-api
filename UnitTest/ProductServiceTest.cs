using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Data;
using ProductWebAPI.DTO;
using ProductWebAPI.Interface;
using ProductWebAPI.Models;
using BusinessLogicLayer.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Helpers;
using ProductWebAPI.Mapper;
using BusinessLogicLayer.Interface;

namespace BusinessLogicLayer.Tests
{
    public class ProductServiceTests
    {
        private readonly DataContext _context;
        private readonly Mock<IProductRepository> _mockProductRepo;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "ProductServiceTestDb")
                .Options;

            _context = new DataContext(options);
            _mockProductRepo = new Mock<IProductRepository>();
            _productService = new ProductService(_context, _mockProductRepo.Object);

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

/*        [Fact]
        public async Task CreateProductAsync_CreatesProduct()
        {
            var productDto = new ProductDTO { Name = "Test Product", Description = "Description", Price = 10 };
            var product = new Product { Id = 3, Name = productDto.Name, Description = productDto.Description, Price = productDto.Price };

            _mockProductRepo.Setup(repo => repo.CreateProduct(It.IsAny<Product>())).ReturnsAsync(new ActionResult<Product>(product));

            var result = await _productService.CreateProductAsync(productDto);

            Assert.Equal(product, result.Value);
        }

        [Fact]
        public async Task CreateProductAsync_ProductAlreadyExists()
        {
            var productDto = new ProductDTO { Name = "Product1", Description = "Description", Price = 10 };

            _mockProductRepo.Setup(repo => repo.ProductExists(productDto.Name)).Returns(true);

            var result = await _productService.CreateProductAsync(productDto);

            var conflictResult = Assert.IsType<ConflictObjectResult>(result.Result);
            Assert.Equal("Product with the same name already exists", conflictResult.Value);
        } */

        [Fact]
        public async Task DeleteProduct_DeletesProduct()
        {
            var product = await _context.Products.Where(p => p.Id == 1).FirstOrDefaultAsync();

            _mockProductRepo.Setup(repo => repo.DeleteProduct(product)).ReturnsAsync(new ActionResult<Product>(product));

            var result = await _productService.DeleteProduct(1);

            Assert.Equal(product, result.Value);
        }

        [Fact]
        public async Task DeleteProduct_DoesntDeleteProduct()
        {
            var product = await _context.Products.Where(p => p.Id == int.MaxValue).FirstOrDefaultAsync();

            _mockProductRepo.Setup(repo => repo.DeleteProduct(product)).ReturnsAsync(new ActionResult<Product>(product));

            Assert.Null(product);
        }

        [Fact]
        public async Task EditProductAsync_UpdatesProduct()
        {
            var productDto = new ProductDTO { Name = "Updated Product", Description = "Updated Description", Price = 20 };
            var product = await _context.Products.Where(p => p.Id == 1).FirstOrDefaultAsync();

            _mockProductRepo.Setup(repo => repo.EditProduct(It.IsAny<Product>())).ReturnsAsync(new ActionResult<Product>(product));

            var result = await _productService.EditProductAsync(1, productDto);

            Assert.Equal("Updated Product", product.Name);
            Assert.Equal("Updated Description", product.Description);
            Assert.Equal(20, product.Price);
        }

        [Fact]
        public async Task EditProductAsync_ProductDoesntExist_ReturnNull()
        {
            var productDto = new ProductDTO { Name = "Updated Product", Description = "Updated Description", Price = 20 };
            var product = await _context.Products.Where(p => p.Id == int.MaxValue).FirstOrDefaultAsync();

            _mockProductRepo.Setup(repo => repo.EditProduct(It.IsAny<Product>())).ReturnsAsync(new ActionResult<Product>(product));

            Assert.Null(product);
        }

        [Fact]
        public async Task GetProductByIdAsync_NotFound_ReturnNull()
        {
            var product = await _context.Products.Where(p => p.Id == int.MaxValue).FirstOrDefaultAsync();

            _mockProductRepo.Setup(repo => repo.GetProductById(int.MaxValue)).ReturnsAsync(new ActionResult<Product>(product));

            Assert.Null(product);
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnsProduct()
        {
            var product = await _context.Products.Where(p => p.Id == 1).FirstOrDefaultAsync();

            _mockProductRepo.Setup(repo => repo.GetProductById(1)).ReturnsAsync(new ActionResult<Product>(product));

            var result = await _productService.GetProductByIdAsync(1);

            Assert.Equal(product, result.Value);
        }


        [Fact]
        public async Task GetProductsAsync_ReturnsFilteredProducts()
        {
            var query = new QueryObject
            {
                Name = "Product1"
            };
            var products = _context.Products.Where(p => p.Name.Contains(query.Name)).ToList();

            _mockProductRepo.Setup(repo => repo.GetProducts(query)).ReturnsAsync(new ActionResult<List<Product>>(products));

            var result = await _productService.GetProductsAsync(query);

            Assert.Single(result.Value);
            Assert.Equal("Product1", result.Value[0].Name);
        }

        [Fact]
        public async Task GetProductAsync_NoProductsWithGiveFilter()
        {
            var query = new QueryObject
            {
                Name = "ewoigfjoiewjfoijfew1"
            };
            var products = _context.Products.Where(p => p.Name.Contains(query.Name)).ToList();

            _mockProductRepo.Setup(repo => repo.GetProducts(query)).ReturnsAsync(new ActionResult<List<Product>>(products));

            var result = await _productService.GetProductsAsync(query);

            Assert.Empty(result.Value);
        }
    }
}
