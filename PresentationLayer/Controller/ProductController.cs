using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Data;
using ProductWebAPI.DTO;
using ProductWebAPI.Mapper;
using ProductWebAPI.Models;
using ProductWebAPI.Repository;

namespace ProductWebAPI.Controller
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository _productRepo;
        private readonly DataContext _context;

        public ProductController(ProductRepository productRepo, DataContext context)
        {
            _productRepo = productRepo;
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var products = await _productRepo.GetProducts();

            if (products == null)
                return NotFound(ModelState);

            return Ok(products);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Product>> GetProductById([FromRoute]int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productRepo.GetProductById(id);

            if (product == null)
                return NotFound(ModelState);

            return Ok(product);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Product>> EditProduct([FromRoute]int id, [FromBody]ProductDTO productChanges)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productRepo.EditProduct(id, productChanges.ToProductFromProductDTO());

            if (product == null)
                return NotFound(ModelState);

            return Ok(product);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Product>> CreateProduct([FromBody]Product NewProduct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productRepo.CreateProduct(NewProduct);

            return Ok(product);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Product>> DeleteProduct([FromRoute]int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();

            if (product == null)
                return NotFound(ModelState);

            var response = _productRepo.DeleteProduct(product);

            return Ok(response);
        }
    }
}
