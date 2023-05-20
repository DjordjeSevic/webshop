using Contracts.Dtos.Brands;
using Contracts.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IBrandService
    {   
        Task<IEnumerable<BrandDto>> GetBrandsAsync();
        Task<BrandDto> GetBrandAsync(Guid id);
    }
}
