namespace ECommerceOrderManagementAPI.DTOs
{
    // Represents an order returned from API endpoints
    public class OrderResponseDTO
    {
        public int OrderId { get; set; }
        public string OrderDate { get; set; } = null!;
        public string Status { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; } = null!;
        public string BillingAddress { get; set; } = null!;
        public CustomerResponseDTO Customer { get; set; } = null!;
        public List<OrderItemResponseDTO> Items { get; set; } = new();
    }
}
