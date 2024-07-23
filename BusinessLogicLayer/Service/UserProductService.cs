using BusinessLogicLayer.Interface;
using BusinessLogicLayer.Service_Interfaces;
using DataAccessLayer.Interface;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using ProductWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service
{
    public class UserProductService : IUserProductService
    {
        private readonly IUserProductRepository _userProductRepo;
        public UserProductService(IUserProductRepository userProductRepo)
        {
            _userProductRepo = userProductRepo;
        }

        public Task<UserProduct> CreateUserProductAsync(int productId, string userId)
        {
            var userProductModel = new UserProduct
            {
                UserId = userId,
                ProductId = productId
            };

            return _userProductRepo.CreateUserProduct(userProductModel); 
        }

        public async Task<List<Product>> GetUserProductsAsync(AppUser user)
        {
            return await _userProductRepo.GetUserProducts(user);
        }
    }
}
