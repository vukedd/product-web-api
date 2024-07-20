﻿using DataAccessLayer.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Data;
using ProductWebAPI.DTO;
using ProductWebAPI.Interface;
using ProductWebAPI.Mapper;
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

        public async Task<ActionResult<List<Product>>> GetProducts(QueryObject query)
        {
            var products = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                products = products.Where(p => p.Name.Contains(query.Name));
            }

            return await products.ToListAsync();
        }

        public bool ProductExists(String name)
        {
            var product = _context.Products.Where(p => p.Name == name).FirstOrDefault();
            if (product == null)
                return false;

            return true;
        }
    }
}
