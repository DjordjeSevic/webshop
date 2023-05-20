using Contracts.Dtos.Products;
using Contracts.Dtos.Brands;
using Contracts.Dtos.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Dtos.Pagination;

namespace Services.Abstractions
{
    public interface IProductService
    {
        Task<PaginationResponse<ProductDto>> GetProductsAsync(ProductSpecParams productParams);
        Task<ProductDto> GetProductAsync(Guid id);
    }
}
