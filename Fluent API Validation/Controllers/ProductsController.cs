using Fluent_API_Validation.Data;
using Fluent_API_Validation.DTOs;
using Fluent_API_Validation.Model;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fluent_API_Validation.Controllers
{
    // The ProductsController handles all product-related API endpoints.
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // The database context used to interact with the underlying database.
        private readonly ECommerceDbContext _context;
        private readonly IValidator<ProductCreateDTO> _createValidator;
        private readonly IValidator<ProductUpdateDTO> _updateValidator;
        // Constructor injection of the database context and validators.
        public ProductsController(
            ECommerceDbContext context,
            IValidator<ProductCreateDTO> createValidator,
            IValidator<ProductUpdateDTO> updateValidator)
        {
            _context = context;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        // GET: api/products?tags=tag1,tag2
        // Retrieves all products, optionally filtered by any matching tags.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponseDTO>>> GetProducts([FromQuery] string? tags)
        {
            // Build the query and include the related Tags collection for each product.
            IQueryable<Product> query = _context.Products
                            .AsNoTracking()
                            .Include(p => p.Tags);
            if (!string.IsNullOrEmpty(tags))
            {
                // Split the comma-separated tags string into a list.
                // Trim spaces and convert each tag to lower case for case-insensitive comparison.
                var tagList = tags.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                  .Select(t => t.Trim().ToLower())
                                  .ToList();
                // Filter products that contain ANY of the specified tags.
                // If a product has at least one matching tag, it is included in the result.
                query = query.Where(p => p.Tags.Any(t => tagList.Contains(t.Name.ToLower())));
            }
            // Execute the query and retrieve the list of products from the database.
            var products = await query.ToListAsync();
            // Map each Product entity to a ProductResponseDTO.
            var result = products.Select(p => new ProductResponseDTO
            {
                ProductId = p.ProductId,
                SKU = p.SKU,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                CategoryId = p.CategoryId,
                Description = p.Description,
                Discount = p.Discount,
                ManufacturingDate = p.ManufacturingDate,
                ExpiryDate = p.ExpiryDate,
                // Map the Tags collection to a list of tag names.
                Tags = p.Tags.Select(t => t.Name).ToList()
            }).ToList();
            // Return the filtered list of products with an HTTP 200 OK status.
            return Ok(result);
        }

        // GET: api/products/{id}
        // Retrieves a single product by its ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDTO>> GetProduct(int id)
        {
            // Retrieve the product with the given ID, including its Tags.
            // AsNoTracking() is used here since no update is needed (improves performance).
            var product = await _context.Products
                                        .AsNoTracking()
                                        .Include(p => p.Tags)
                                        .FirstOrDefaultAsync(p => p.ProductId == id);

            // If the product is not found, return a 404 Not Found response.
            if (product == null)
                return NotFound();
            // Map the Product entity to a ProductResponseDTO.
            var response = new ProductResponseDTO
            {
                ProductId = product.ProductId,
                SKU = product.SKU,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                Description = product.Description,
                Discount = product.Discount,
                ManufacturingDate = product.ManufacturingDate,
                ExpiryDate = product.ExpiryDate,
                Tags = product.Tags.Select(t => t.Name).ToList()
            };
            // Return the product details with an HTTP 200 OK status.
            return Ok(response);
        }

        // POST: api/products
        // Creates a new product based on the data provided in ProductCreateDTO.
        [HttpPost]
        public async Task<ActionResult<ProductResponseDTO>> CreateProduct([FromBody] ProductCreateDTO productDto)
        {
            // Validate asynchronously using FluentValidation
            var validationResult = await _createValidator.ValidateAsync(productDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new
                {
                    Field = e.PropertyName,
                    Error = e.ErrorMessage
                });
                return BadRequest(new { Errors = errors });
            }

            // Map the incoming DTO to a new Product entity.
            var product = new Product
            {
                SKU = productDto.SKU,
                Name = productDto.Name,
                Price = productDto.Price,
                Stock = productDto.Stock,
                CategoryId = productDto.CategoryId,
                Description = productDto.Description,
                Discount = productDto.Discount,
                ManufacturingDate = productDto.ManufacturingDate,
                ExpiryDate = productDto.ExpiryDate
            };
            // Process Tags: For each tag provided in the DTO, check if it exists.
            // If the tag exists, use the existing Tag; otherwise, create a new Tag.
            if (productDto.Tags != null && productDto.Tags.Any())
            {
                foreach (var tagName in productDto.Tags)
                {
                    // Normalize the tag by trimming whitespace and converting to lower case.
                    var normalizedTagName = tagName.Trim().ToLower();
                    var existingTag = await _context.Tags.FirstOrDefaultAsync(t => t.Name.ToLower() == normalizedTagName);
                    if (existingTag != null)
                        product.Tags.Add(existingTag); // Associate the existing tag with the product.
                    else
                        product.Tags.Add(new Tag { Name = normalizedTagName }); // Create a new tag and associate it.
                }
            }
            // Add the new product to the database context.
            _context.Products.Add(product);
            // Save changes to persist the new product (and associated tags) to the database.
            await _context.SaveChangesAsync();
            // Map the newly created Product entity to a ProductResponseDTO.
            var response = new ProductResponseDTO
            {
                ProductId = product.ProductId,
                SKU = product.SKU,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                Description = product.Description,
                Discount = product.Discount,
                ManufacturingDate = product.ManufacturingDate,
                ExpiryDate = product.ExpiryDate,
                Tags = product.Tags.Select(t => t.Name).ToList()
            };
            // Return the created product with an HTTP 200 OK (or 201 Created) status.
            return Ok(response);
        }

        // PUT: api/products/{id}
        // Updates an existing product based on the data provided in ProductUpdateDTO.
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductResponseDTO>> UpdateProduct(int id, [FromBody] ProductUpdateDTO productDto)
        {
            // Verify that the ID provided in the URL matches the ID in the request body.
            if (id != productDto.ProductId)
                return BadRequest(new { error = "Product ID in URL and body do not match." });
            // Validate asynchronously using FluentValidation
            var validationResult = await _updateValidator.ValidateAsync(productDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new
                {
                    Field = e.PropertyName,
                    Error = e.ErrorMessage
                });
                return BadRequest(new { Errors = errors });
            }
            // Retrieve the existing product from the database, including its associated Tags.
            var product = await _context.Products
                                        .Include(p => p.Tags)
                                        .FirstOrDefaultAsync(p => p.ProductId == id);

            // If the product does not exist, return a 404 Not Found response.
            if (product == null)
                return NotFound();
            // Update the product properties with the new values from the DTO.
            product.SKU = productDto.SKU;
            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.Stock = productDto.Stock;
            product.CategoryId = productDto.CategoryId;
            product.Description = productDto.Description;
            product.Discount = productDto.Discount;
            product.ManufacturingDate = productDto.ManufacturingDate;
            product.ExpiryDate = productDto.ExpiryDate;
            // Clear the existing Tags associated with the product.
            product.Tags.Clear();
            // Process Tags: For each tag provided in the DTO, normalize and add it.
            if (productDto.Tags != null && productDto.Tags.Any())
            {
                foreach (var tagName in productDto.Tags)
                {
                    // Normalize the tag by trimming whitespace and converting to lower case.
                    var normalizedTagName = tagName.Trim().ToLower();
                    var existingTag = await _context.Tags.FirstOrDefaultAsync(t => t.Name.ToLower() == normalizedTagName);
                    if (existingTag != null)
                        product.Tags.Add(existingTag); // Associate the existing tag with the product.
                    else
                        product.Tags.Add(new Tag { Name = normalizedTagName }); // Create and add a new tag.
                }
            }
            // Mark the product entity as updated in the database context.
            _context.Products.Update(product);
            // Save changes to persist the updated product (and tag associations) to the database.
            await _context.SaveChangesAsync();
            // Map the updated product entity to a ProductResponseDTO.
            var response = new ProductResponseDTO
            {
                ProductId = product.ProductId,
                SKU = product.SKU,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                Description = product.Description,
                Discount = product.Discount,
                ManufacturingDate = product.ManufacturingDate,
                ExpiryDate = product.ExpiryDate,
                Tags = product.Tags.Select(t => t.Name).ToList()
            };
            // Return the updated product with an HTTP 200 OK status.
            return Ok(response);
        }
    }
}
