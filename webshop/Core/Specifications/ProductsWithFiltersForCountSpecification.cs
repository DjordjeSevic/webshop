using Contracts.Dtos.Products;
using Domain.Entities;

namespace Domain.Specifications
{
    public class ProductsWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductsWithFiltersForCountSpecification(ProductSpecParams productParams)
            : base(x => (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
                        (!productParams.BrandId.HasValue || x.BrandId == productParams.BrandId) &&
                        (!productParams.CategoryId.HasValue || x.CategoryId == productParams.CategoryId)
            )
        {
        }
    }
}
