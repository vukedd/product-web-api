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
    [Authorize]
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
        public async Task<IActionResult> GetUserProducts()
        {
            var userId = User.GetId();
            var appUser = await _userManager.FindByIdAsync(userId);
            var userProducts = await _userProductService.GetUserProductsAsync(appUser);

            return Ok(userProducts);
        }

        // Da li prozivod postoji?
        // Da li je ulogovan korisnik vlasnik tog proizvoda?
        // Da li postoji vec element u join tabeli sa ta dva entiteta?

        [HttpPost]
        public async Task<IActionResult> CreateUserProducts([FromQuery] int ProductId, [FromQuery]string UserId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = _productService.GetProductByIdAsync(ProductId);
            if (product == null)
                return NotFound();

            var ProductAssignerId = User.GetId();
            var ProductAssigner = await _userManager.FindByIdAsync(ProductAssignerId);

            var productList = await _userProductService.GetUserProductsAsync(ProductAssigner);

            var FindProduct = productList.Where(p => p.Id == ProductId).FirstOrDefault();
            if (FindProduct == null)
                return BadRequest("You cannot assign a product u don't own!");

            var UserToAssign = await _userManager.FindByIdAsync(UserId);
            var AssigneProductList = await _userProductService.GetUserProductsAsync(UserToAssign);
            var ExistingCheck = AssigneProductList.Where(p => p.Id == ProductId).FirstOrDefault();
            if (ExistingCheck != null)
                return BadRequest("Product already assigned to selected User!");

            var response = await _userProductService.CreateUserProductAsync(ProductId, UserId);

            return Ok("Product successfully assigned!");

        }
/*
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteUserProduct([FromQuery])
        {

        }
*/
    }

}