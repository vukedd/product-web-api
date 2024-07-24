using DataAccessLayer.Interface;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Data;
using ProductWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class UserProductRepository : IUserProductRepository
    {
        private readonly DataContext _context;
        public UserProductRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetUserProducts(AppUser user)
        {
            return await _context.UserProducts.Where(u => u.UserId == user.Id).Select(product => new Product
            {
                Id = product.ProductId,
                Name = product.Product.Name,
                Description = product.Product.Description,
                Price = product.Product.Price
            }).ToListAsync();
        }

        public async Task<UserProduct> CreateUserProduct(UserProduct userProduct)
        {
            await _context.AddAsync(userProduct);
            await _context.SaveChangesAsync();

            return userProduct;
        }

        public async Task<UserProduct> DeleteUserProduct(UserProduct userProductForDeletion)
        {
            _context.Remove(userProductForDeletion);
            await _context.SaveChangesAsync();

            return userProductForDeletion;
        }

        public async Task<int> GetUserProductCount()
        {
            return await _context.UserProducts.CountAsync();
        }
    }
}
