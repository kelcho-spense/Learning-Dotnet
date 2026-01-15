namespace Fluent_API_Validation.DTOs
{
    // ProductUpdateDTO inherits all common properties from ProductBaseDTO
    // and adds the ProductId for identifying the product to update
    public class ProductUpdateDTO : ProductBaseDTO
    {
        public int ProductId { get; set; }
    }
}
