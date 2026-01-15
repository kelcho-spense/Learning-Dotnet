using Fluent_API_Validation.Data;
using Fluent_API_Validation.DTOs;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Fluent_API_Validation.Validators
{
    /// <summary>
    /// Validator for ProductUpdateDTO, inherits all common rules from ProductBaseDTOValidator.
    /// Includes async validation to check if ProductId exists in database.
    /// </summary>
    public class ProductUpdateDTOValidator : ProductBaseDTOValidator<ProductUpdateDTO>
    {
        public ProductUpdateDTOValidator(ECommerceDbContext context) : base(context)
        {
            // ProductId validation: must be greater than 0 and exist in database
            RuleFor(p => p.ProductId)
                .GreaterThan(0).WithMessage("ProductId must be greater than 0.")
                .MustAsync(ProductExistsAsync).WithMessage("Product does not exist.");
        }

        /// <summary>
        /// Checks asynchronously that the ProductId exists in the database.
        /// </summary>
        private async Task<bool> ProductExistsAsync(int productId, CancellationToken cancellationToken)
        {
            return await _context.Products
                .AsNoTracking()
                .AnyAsync(p => p.ProductId == productId, cancellationToken);
        }

        /// <summary>
        /// Override to exclude the current product when checking SKU uniqueness during updates.
        /// </summary>
        protected override async Task<bool> BeUniqueSKUAsync(ProductUpdateDTO dto, string sku, CancellationToken cancellationToken)
        {
            return !await _context.Products
                .AsNoTracking()
                .AnyAsync(p => p.SKU == sku && p.ProductId != dto.ProductId, cancellationToken);
        }

        /// <summary>
        /// Override to exclude the current product when checking Name uniqueness during updates.
        /// </summary>
        protected override async Task<bool> BeUniqueNameAsync(ProductUpdateDTO dto, string name, CancellationToken cancellationToken)
        {
            return !await _context.Products
                .AsNoTracking()
                .AnyAsync(p => p.Name == name && p.ProductId != dto.ProductId, cancellationToken);
        }
    }
}
