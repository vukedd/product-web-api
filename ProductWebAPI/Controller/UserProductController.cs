using BusinessLogicLayer.Extensions;
using BusinessLogicLayer.Interface;
using BusinessLogicLayer.Service_Interfaces;
using DataAccessLayer.Models;
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
            var userId = User.GetId();
            var appUser = await _userManager.FindByIdAsync(userId);
            var userProducts = await _userProductService.GetUserProductsAsync(appUser);

            return Ok(userProducts);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUserProducts([FromQuery] int ProductId)
        {
            var userId = User.GetId();
            var user = await _userManager.FindByIdAsync(userId);

            var product = _productService.GetProductByIdAsync(ProductId);
            if (product == null)
                return BadRequest(ModelState);

            var products = await _userProductService.GetUserProductsAsync(user);

            if (products.Any(p => p.Id == ProductId))
                return BadRequest("User already connected with this product.");

            var userProduct = _userProductService.CreateUserProductAsync(ProductId, userId);

            return Ok(userProduct);
        }

    }

}