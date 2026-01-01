using Microsoft.Extensions.Caching.Memory;
using ShoppingCartAPI.Models;
using ShoppingCartAPI.Services.Interfaces;

namespace ShoppingCartAPI.Services
{
    public class CartService : ICartService
    {
        private readonly IMemoryCache _cache;
        private readonly string _userId;
        private readonly ILogger<CartService> _logger;

        public CartService(IMemoryCache cache, IHttpContextAccessor httpContextAccessor, ILogger<CartService> logger)
        {
            _cache = cache;
            _userId = httpContextAccessor.HttpContext?.Request.Headers["UserId"].FirstOrDefault() ?? "guest";
            _logger = logger;
            _logger.LogInformation("CartService (Scoped) instance created for user {UserId}", _userId);
        }

        public void AddItem(CartItem item)
        {
            _logger.LogInformation("Adding item for user {UserId}: {ProductName}", _userId, item.ProductName);

            var cart = _cache.GetOrCreate(_userId, entry =>
            {
                _logger.LogInformation("Creating new cart for user {UserId}", _userId);
                entry.SlidingExpiration = TimeSpan.FromMinutes(30);
                return new List<CartItem>();
            })!;

            var existing = cart.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (existing != null)
            {
                existing.Quantity += item.Quantity;
                _logger.LogInformation("Updated quantity for ProductId {ProductId}, new quantity: {Quantity}", item.ProductId, existing.Quantity);
            }
            else
            {
                cart.Add(item);
                _logger.LogInformation("Added new item. Cart now has {Count} items", cart.Count);
            }

            _cache.Set(_userId, cart);
            _logger.LogInformation("Cart saved to cache for user {UserId}, total items: {Count}", _userId, cart.Count);
        }

        public List<CartItem> GetItems()
        {
            _logger.LogInformation("Getting items for user {UserId}", _userId);

            if (_cache.TryGetValue(_userId, out List<CartItem>? cart))
            {
                _logger.LogInformation("Cart found for user {UserId}, contains {Count} items", _userId, cart?.Count ?? 0);
                return cart ?? new List<CartItem>();
            }

            _logger.LogWarning("No cart found in cache for user {UserId}", _userId);
            return new List<CartItem>();
        }

        public void ClearCart()
        {
            _logger.LogInformation("Clearing cart for user {UserId}", _userId);
            _cache.Remove(_userId);
        }
    }
}
