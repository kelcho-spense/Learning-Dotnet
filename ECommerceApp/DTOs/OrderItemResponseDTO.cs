namespace ECommerceOrderManagementAPI.DTOs
{
    // Represents an item inside an order when returning data
    public class OrderItemResponseDTO
    {
        public string ProductName { get; set; } = null!;
        public string Category { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
