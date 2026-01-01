using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Services.Interfaces
{
    public interface ICartSummaryService
    {
        CartSummary GenerateSummary();
    }
}
