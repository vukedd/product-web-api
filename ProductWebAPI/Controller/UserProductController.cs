using BusinessLogicLayer.Extensions;
using BusinessLogicLayer.Interface;
using BusinessLogicLayer.Service_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.Interface;
using ProductWebAPI.Models;

namespace PresentationLayer.Controller
{
    [Route("api/userProducts")]
    [ApiController]
    public class UserProductController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IProductService _productService;
        private readonly IUserProductService _userProductService;
        public UserProductController(UserManager<AppUser> userManager, IProductService productService, IUserProductService userProductService)
        {
            _userManager = userManager;
            _productService = productService;
            _userProductService = userProductService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserProducts()
        {
            // User je nasledjen iz controlerBase-a i on se odnosi na korisnika u sesiji
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userProducts = await _userProductService.GetUserProductsAsync(appUser);

            return Ok(userProducts);
        }
    }
}
