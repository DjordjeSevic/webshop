using Contracts.Dtos.Brands;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class BrandsController : BaseApiController
    {
        private readonly IServiceManager _serviceManager;
        public BrandsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        {
            var brands = await _serviceManager.BrandService.GetBrandsAsync();

            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BrandDto>> GetBrand(Guid id)
        {
            var brand = await _serviceManager.BrandService.GetBrandAsync(id);

            return Ok(brand);
        }
    }
}
