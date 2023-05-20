using Contracts.Dtos.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Contracts.Dtos.Errors;
using Services.Abstractions;
using Contracts.Dtos.Pagination;

namespace Presentation.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IServiceManager _serviceManager;
        public ProductsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<PaginationResponse<ProductDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            var products = await _serviceManager.ProductService.GetProductsAsync(productParams);

            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
        {
            var product = await _serviceManager.ProductService.GetProductAsync(id);

            if (product == null) return NotFound(new ApiResponse(404));

            return Ok(product);
        }
    }
}
