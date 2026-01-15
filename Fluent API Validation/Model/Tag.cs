namespace Fluent_API_Validation.Model
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        // Many-to-many relationship with Product
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
