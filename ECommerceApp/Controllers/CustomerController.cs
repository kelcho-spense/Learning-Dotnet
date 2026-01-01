using ECommerceOrderManagementAPI.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECommerceOrderManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CustomerController(AppDbContext context) 
        {
            _context = context; 
               
        }

        // GET: api/<CustomerController>
        [HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<CustomerController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            // STEP 1: Fetch only the Customer entity by primary key.
            // Lazy loading ensures that related entities are NOT loaded at this point.
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound($"Customer with ID {id} not found.");
            // ==================================================================
            // STEP 2: Accessing navigation properties below triggers
            // automatic SQL queries via EF Core’s proxy objects.
            // Each navigation property is loaded only when first accessed.
            // ==================================================================
            // Load Profile automatically when accessed
            // EF triggers a SELECT for Profile table
            var profile = customer.Profile;
            // Load Addresses automatically when accessed
            // EF triggers a SELECT for Addresses table
            var addresses = customer.Addresses;
            // Load Orders automatically when accessed
            // EF triggers a SELECT for Orders table
            var orders = customer.Orders;
            // ==================================================================
            // STEP 3: Construct an Anonymous Object with all details.
            // Related entities are now fully available because EF Core
            // has executed necessary SQL queries automatically.
            // ==================================================================
            var result = new
            {
                customer.CustomerId,
                customer.Name,
                customer.Email,
                customer.Phone,
                Profile = profile == null ? null : new
                {
                    profile.DisplayName,
                    profile.Gender,
                    DateOfBirth = profile.DateOfBirth.ToString("yyyy-MM-dd")
                },
                Addresses = addresses?.Select(a => new
                {
                    a.AddressId,
                    a.Line1,
                    a.Street,
                    a.City,
                    a.PostalCode,
                    a.Country
                }).ToList(),
                Orders = orders?.Select(o => new
                {
                    o.OrderId,
                    OrderDate = o.OrderDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    o.TotalAmount,
                    Status = o.OrderStatus?.Name, // Lazy load triggers OrderStatus if accessed
                    Items = o.OrderItems?.Select(i => new
                    {
                        i.ProductId,
                        ProductName = i.Product.Name,        // Triggers lazy load of Product
                        Category = i.Product.Category.Name,  // Triggers lazy load of Category
                        i.Quantity,
                        i.UnitPrice
                    })
                }).ToList()

            };
            // ==================================================================
            // STEP 4: Return structured anonymous object.
            // At this point, EF Core has loaded all the required related data
            // lazily and efficiently, only when it was accessed.
            // ==================================================================
            return Ok(result);

        }

        // POST api/<CustomerController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<CustomerController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<CustomerController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
