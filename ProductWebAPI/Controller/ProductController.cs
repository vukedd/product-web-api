﻿using BusinessLogicLayer.Service;
using DataAccessLayer.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Data;
using ProductWebAPI.DTO;
using ProductWebAPI.Mapper;
using ProductWebAPI.Models;
using ProductWebAPI.Interface;
using BusinessLogicLayer.Interface;
using BusinessLogicLayer.Extensions;
using Microsoft.AspNetCore.Identity;
using BusinessLogicLayer.DTO;
using System.Numerics;

namespace ProductWebAPI.Controller
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ProductController(IProductService productService, DataContext context, UserManager<AppUser> userManager)
        {
            _productService = productService;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var products = await _productService.GetProductsAsync(query);

            if (products == null)
                return NotFound(ModelState);

            return Ok(products);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> GetProductById([FromRoute]int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productService.GetProductByIdAsync(id);

            if (product.Value == null)
                return NotFound(ModelState);

            return Ok(product);
        }

        [HttpGet("statistics")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductStatsDTO>> GetProductStats()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ProductCount = await _productService.GetProductCountAsync();
            var ProductsAveragePrice = await _productService.GetProductsAveragePriceAsync();
            var ProductMinimumPrice = await _productService.GetProductsMinimumPriceAsync();
            var ProductMaximumPrice = await _productService.GetProductMaximumPriceAsync();


            return Ok(new ProductStatsDTO
            {
                ProductCount = ProductCount.Value,
                ProductsAveragePrice = ProductsAveragePrice.Value,
                ProductMaximumPrice = ProductMaximumPrice.Value,
                ProductMinimumPrice = ProductMinimumPrice.Value
            });
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> EditProduct([FromRoute]int id, [FromBody]ProductDTO productChanges)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productService.EditProductAsync(id, productChanges);

            if (product == null)
                return NotFound(ModelState);

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody]ProductDTO NewProduct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetId();
            var user = _userManager.FindByIdAsync(userId);

            var product = await _productService.CreateProductAsync(NewProduct, userId);

            if (product.Value != null)
            {
                return Ok("Product sucessfully created!");
            }

            return StatusCode(409, "Product already exists!");
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> DeleteProduct([FromRoute]int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetId();

            var product = await _productService.DeleteProduct(id, userId);

            if (product == null)
                return BadRequest("The product isn't owned by you or it doesn't exist!");

            return Ok(product);
        }
    }
}
