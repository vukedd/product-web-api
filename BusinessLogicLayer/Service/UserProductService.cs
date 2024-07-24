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

        public Task<UserProduct> CreateUserProductAsync(int productId, string UserId)
        {
            
            var userProductModel = new UserProduct
            {
                UserId = UserId,
                ProductId = productId
            };

            return _userProductRepo.CreateUserProduct(userProductModel); 
        }

        public Task<UserProduct> DeleteUserProductAsync(UserProduct userProduct)
        {
            return _userProductRepo.DeleteUserProduct(userProduct);
        }

        public async Task<int> GetUserProductCount()
        {
            return await _userProductRepo.GetUserProductCount();
        }

        public async Task<ICollection<Product>> GetUserProductsAsync(AppUser user)
        {
            return await _userProductRepo.GetUserProducts(user);
        }
    }
}
