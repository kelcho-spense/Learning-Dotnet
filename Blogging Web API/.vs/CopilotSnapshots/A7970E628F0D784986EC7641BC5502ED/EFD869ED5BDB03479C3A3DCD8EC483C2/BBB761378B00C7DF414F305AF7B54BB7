using Microsoft.AspNetCore.Mvc;
using small_ecommerce_api.DTOs;
using small_ecommerce_api.Services;

namespace small_ecommerce_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/products
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> GetProducts()
        {
            var Products = _productService.GetAllProducts();

            if (Products == null)
            {
                return NotFound(new { Message = "No products found." });
            }

            return Ok(Products);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public ActionResult<ProductDTO> GetProduct(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found." });
            }

            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public ActionResult<ProductDTO> PostProduct([FromBody] ProductCreateDTO createDto)
        {
            var newProduct = _productService.CreateProduct(createDto);

            return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct);
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductUpdateDTO updateDto)
        {
            if (id != updateDto.Id)
            {
                return BadRequest(new { Message = "ID mismatch between route and body." });
            }

            var existingProduct = _productService.GetProductById(id);

            if (existingProduct == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found." });
            }

            return NoContent();
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found." });
            }

            return NoContent();
        }


    }
}
