using small_ecommerce_api.DTOs;

namespace small_ecommerce_api.Services
{
    public interface IProductService
    {
        IEnumerable<ProductDTO> GetAllProducts();
        ProductDTO? GetProductById(int id);
        ProductDTO CreateProduct(ProductCreateDTO createDto);
        bool UpdateProduct(int id, ProductUpdateDTO updateDto);
        bool DeleteProduct(int id);
    }
}
