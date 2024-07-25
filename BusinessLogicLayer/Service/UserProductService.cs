using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interface;
using BusinessLogicLayer.Service_Interfaces;
using DataAccessLayer.Helpers;
using DataAccessLayer.Interface;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Interface;
using ProductWebAPI.Mapper;
using ProductWebAPI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service
{
    public class UserProductService : IUserProductService
    {
        private readonly IUserProductRepository _userProductRepo;
        private readonly IProductRepository _productRepo;
        private readonly UserManager<AppUser> _userManager;
        public UserProductService(IUserProductRepository userProductRepo, IProductRepository productRepo, UserManager<AppUser> userManager)
        {
            _userProductRepo = userProductRepo;
            _productRepo = productRepo;
            _userManager = userManager;
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

        public async Task<List<ProductPopularityDTO>> GetProductPopularityList(QueryObjectUserProduct query)
        {
            Dictionary<int, int> UserProductCounts = new Dictionary<int, int>();
            var AllUserProducts = await _userProductRepo.GetAllUserProducts();

            foreach(UserProduct UserProduct in AllUserProducts)
            {
                if (!UserProductCounts.ContainsKey(UserProduct.ProductId))
                    UserProductCounts.Add(UserProduct.ProductId, 1);
                else 
                    UserProductCounts[UserProduct.ProductId] += 1;  
            }

            List<ProductPopularityDTO> ProductList = new List<ProductPopularityDTO>();
            foreach (var el in UserProductCounts.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value))
            {
                var product = await _productRepo.GetProductById(el.Key);
                var owner = await _userManager.FindByIdAsync(product.Value.OwnerId);
                ProductList.Add(product.Value.ToProductPopularityDTO(el.Value, owner.NormalizedUserName));

                if (query.ListSize == 0)
                {
                    if (ProductList.Count() == 5)
                        break;
                }
                else
                {
                    if (ProductList.Count() == query.ListSize)
                        break;
                }
                
            }

            return ProductList;

        }

        public async Task<int> GetUserProductCount()
        {
            return await _userProductRepo.GetUserProductCount();
        }

        public async Task<ICollection<Product>> GetUserProductsAsync(AppUser user)
        {
            return await _userProductRepo.GetUserProducts(user);
        }
        
        private async Task<ICollection<UserProduct>> GetAllUserProducts()
        {
            return await _userProductRepo.GetAllUserProducts();
        }
    }
}
