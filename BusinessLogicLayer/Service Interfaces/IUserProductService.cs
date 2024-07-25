using BusinessLogicLayer.DTO;
using DataAccessLayer.Helpers;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ProductWebAPI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service_Interfaces
{
    public interface IUserProductService
    {
        public Task<ICollection<Product>> GetUserProductsAsync(AppUser user);
        public Task<int> GetUserProductCount();
        public Task<List<ProductPopularityDTO>> GetProductPopularityList(QueryObjectUserProduct ListSize);
        public Task<UserProduct> CreateUserProductAsync(int productId, string UserId);
        public Task<UserProduct> DeleteUserProductAsync(UserProduct userProduct);
    }
}
