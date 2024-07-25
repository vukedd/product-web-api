using DataAccessLayer.Models;
using ProductWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface
{
    public interface IUserProductRepository
    {
        Task<List<Product>> GetUserProducts(AppUser user);
        Task<int> GetUserProductCount();
        Task<ICollection<UserProduct>> GetAllUserProducts();

        Task<UserProduct> CreateUserProduct(UserProduct userProduct);
        Task<UserProduct> DeleteUserProduct(UserProduct userProductForDeletion);
    }
}
