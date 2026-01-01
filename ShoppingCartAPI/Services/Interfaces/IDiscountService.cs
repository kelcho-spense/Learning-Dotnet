namespace ShoppingCartAPI.Services.Interfaces
{
    public interface IDiscountService
    {
        decimal CalculateDiscount(decimal amount);
    }
}
