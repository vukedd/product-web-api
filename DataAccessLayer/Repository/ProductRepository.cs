using DataAccessLayer.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Data;
using ProductWebAPI.Interface;
using ProductWebAPI.Models;

namespace ProductWebAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<Product>> CreateProduct(Product NewProduct)
        { 
            await _context.Products.AddAsync(NewProduct);
            await _context.SaveChangesAsync();

            return NewProduct;
        }

        public async Task<ActionResult<Product>> DeleteProduct(Product ProductForDeletion)
        {
            _context.Products.Remove(ProductForDeletion);
            await _context.SaveChangesAsync();

            return ProductForDeletion;
        }

        public async Task<ActionResult<Product>> EditProduct(Product productForChange)
        {

            _context.Update(productForChange);
            await _context.SaveChangesAsync();

            return productForChange;
        }

        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            return await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public int GetProductCount()
        {
            var AllProducts = _context.Products.ToList();
            return AllProducts.Count();
        }

        public async Task<ActionResult<List<Product>>> GetProducts(QueryObject query)
        {
            var products = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                products = products.Where(p => p.Name.Contains(query.Name));
            }

            return await products.ToListAsync();
        }

        public decimal GetProductsAveragePrice()
        {
            var productPriceSum = _context.Products.Sum(p => (int)p.Price);
            var productCount = GetProductCount();

            return productPriceSum / productCount;
        }

        public bool ProductExists(string name)
        {
            var product = _context.Products.Where(p => p.Name.ToLower() == name.ToLower()).FirstOrDefault();
            if (product != null)
                return true;

            return false;
        }

        public async Task<ActionResult<decimal>> GetProductsMinimumPrice()
        {
            var minProductPrice = await _context.Products.MinAsync(p => (int)p.Price);
            return minProductPrice;
        }

        public async Task<ActionResult<decimal>> GetProductMaximumPrice()
        {
            var maxProductPrice = await _context.Products.MaxAsync(p => (int)p.Price);
            return maxProductPrice;
        }
    }
}
