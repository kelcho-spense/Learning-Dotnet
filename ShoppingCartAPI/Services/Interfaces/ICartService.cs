using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Services.Interfaces
{
    public interface ICartService
    {
        void AddItem(CartItem item);
        List<CartItem> GetItems();
        void ClearCart();
    }
}
