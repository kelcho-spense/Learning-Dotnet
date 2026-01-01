using ShoppingCartAPI.Services.Interfaces;

namespace ShoppingCartAPI.Services
{
    public class AppConfigService : IAppConfigService
    {
        private readonly decimal _taxRate = 0.18m; // GST rate = 18%

        public AppConfigService(ILogger<AppConfigService> logger)
        {
            // Log only once since Singleton → single instance shared across app
            logger.LogInformation("AppConfigService (Singleton) instance created.");
        }

        public decimal GetDeliveryFee(decimal orderAmount)
        {

            if (orderAmount < 500)
                return 50;  // Flat ₹50 for small orders
            else if (orderAmount >= 500 && orderAmount <= 2000)
                return 30;  // Reduced fee for mid-sized orders
            else
                return 0;   // Free delivery for big orders
        }

        public decimal GetTaxRate()
        {
            return _taxRate;
        }
    }
}
