using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using RedisCachingDemo.Data;
using RedisCachingDemo.Models;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedisCachingDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
            private readonly ApplicationDbContext _context;
            private readonly IDistributedCache _cache;
            // Inject ApplicationDbContext and IDistributedCache via constructor.
            public ProductsController(ApplicationDbContext context, IDistributedCache cache)
            {
                _context = context;
                _cache = cache;
            }

            // GET: api/products/all
            [HttpGet("all")]
            public async Task<IActionResult> GetAll()
            {
                var cacheKey = "GET_ALL_PRODUCTS";
                List<Product> products;

                try
                {
                    // Attempt to retrieve the product list from Redis cache.
                    var cachedData = await _cache.GetStringAsync(cacheKey);
                    if (!string.IsNullOrEmpty(cachedData))
                    {
                        // Deserialize JSON string back to List<Product>.
                        products = JsonSerializer.Deserialize<List<Product>>(cachedData) ?? new List<Product>();
                    }
                    else
                    {
                        // Cache miss: fetch products from the database.
                        products = await _context.Products.AsNoTracking().ToListAsync();
                        if (products != null)
                        {
                            // Serialize the product list to a JSON string.
                            var serializedData = JsonSerializer.Serialize(products);
                            // Define cache options (using sliding expiration).
                            var cacheOptions = new DistributedCacheEntryOptions()
                                .SetSlidingExpiration(TimeSpan.FromMinutes(5));
                            // Store the serialized data in Redis.
                            await _cache.SetStringAsync(cacheKey, serializedData, cacheOptions);
                        }
                    }
                    return Ok(products);
                }
                catch (Exception ex)
                {
                    // Return a 500 response if any error occurs.
                    return StatusCode(500, new { message = "An error occurred while retrieving products.", details = ex.Message });
                }
            }

        // GET: api/products/Category?Category=Fruits
        [HttpGet("Category")]
        public async Task<IActionResult> GetProductByCategory(string Category)
        {
            // Cache key includes the category to ensure unique cache entries.
            var cacheKey = $"PRODUCTS_{Category}";
            List<Product> products;
            try
            {
                var cachedData = await _cache.GetStringAsync(cacheKey);
                if (!string.IsNullOrEmpty(cachedData))
                {
                    products = JsonSerializer.Deserialize<List<Product>>(cachedData) ?? new List<Product>();
                }
                else
                {
                    // Cache miss: fetch from the database by matching category.
                    products = await _context.Products
                        .Where(prd => prd.Category.ToLower() == Category.ToLower())
                        .AsNoTracking()
                        .ToListAsync();
                    if (products != null)
                    {
                        var serializedData = JsonSerializer.Serialize(products);
                        // Use absolute expiration so that the cache entry expires after 5 minutes.
                        var cacheOptions = new DistributedCacheEntryOptions()
                            .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                        await _cache.SetStringAsync(cacheKey, serializedData, cacheOptions);
                    }
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving products.", details = ex.Message });
            }
        }
        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            // Cache key for a single product.
            var cacheKey = $"Product_{id}";
            Product? product;
            try
            {
                var cachedData = await _cache.GetStringAsync(cacheKey);
                if (!string.IsNullOrEmpty(cachedData))
                {
                    product = JsonSerializer.Deserialize<Product>(cachedData) ?? new Product();
                }
                else
                {
                    // Fetch from database if not present in cache.
                    product = await _context.Products.FindAsync(id);
                    if (product == null)
                        return NotFound($"Product with ID {id} not found.");
                    var serializedData = JsonSerializer.Serialize(product);
                    await _cache.SetStringAsync(cacheKey, serializedData, new DistributedCacheEntryOptions
                    {
                        SlidingExpiration = TimeSpan.FromMinutes(5)
                    });
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the product.", details = ex.Message });
            }
        }
        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            if (id != updatedProduct.Id)
            {
                return BadRequest("Product ID mismatch.");
            }
            try
            {
                var existingProduct = await _context.Products.FindAsync(id);
                if (existingProduct == null)
                    return NotFound($"Product with ID {id} not found.");
                // Update product details in the database.
                _context.Entry(existingProduct).CurrentValues.SetValues(updatedProduct);
                await _context.SaveChangesAsync();
                // Update the cache for this product.
                var cacheKey = $"Product_{id}";
                var serializedData = JsonSerializer.Serialize(updatedProduct);
                await _cache.SetStringAsync(cacheKey, serializedData, new DistributedCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the product.", details = ex.Message });
            }
        }
        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                    return NotFound($"Product with ID {id} not found.");
                // Remove product from the database.
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                // Remove product from the cache.
                var cacheKey = $"Product_{id}";
                await _cache.RemoveAsync(cacheKey);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the product.", details = ex.Message });
            }
        }
    }
}
