using BusinessLogicLayer.Service_Interfaces;
using DataAccessLayer.Interface;
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
        public async Task<List<Product>> GetUserProductsAsync(AppUser user)
        {
            return await _userProductRepo.GetUserProducts(user);
        }
    }
}
