using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service_Interfaces
{
    public interface IUserProductService
    {
        public Task<List<Product>> GetUserProductsAsync(AppUser user);
    }
}
