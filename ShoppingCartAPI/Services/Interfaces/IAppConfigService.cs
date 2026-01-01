namespace ShoppingCartAPI.Services.Interfaces
{
    public interface IAppConfigService
    {
        decimal GetTaxRate();
        decimal GetDeliveryFee(decimal orderAmount);
    }
}
