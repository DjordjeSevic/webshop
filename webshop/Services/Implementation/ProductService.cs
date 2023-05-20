using Contracts.Dtos.Brands;
using Contracts.Dtos.Categories;
using Contracts.Dtos.Products;
using Contracts.Dtos.Pagination;
using Domain.Entities;
using Domain.Repositories;
using Domain.Specifications;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Services.Implementation
{
    internal sealed class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _repositoryProducts;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> repositoryProducts, IMapper mapper)
        {
            _repositoryProducts = repositoryProducts;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<ProductDto>> GetProductsAsync(ProductSpecParams productParams)
        {
            var spec = new ProductsWithCategoriesAndBrandsSpecification(productParams);

            var countSpec = new ProductsWithFiltersForCountSpecification(productParams);

            var products = await _repositoryProducts.GetAsync(spec);

            var totalItems = await _repositoryProducts.CountAsync(countSpec);

            return new PaginationResponse<ProductDto>(productParams.PageIndex, productParams.PageSize, totalItems, _mapper.Map<IEnumerable<ProductDto>>(products));
        }
        public async Task<ProductDto> GetProductAsync(Guid id)
        {
            var spec = new ProductsWithCategoriesAndBrandsSpecification(id);

            var product = await _repositoryProducts.GetEntityWithSpec(spec);

            return _mapper.Map<ProductDto>(product);
        }
    }
}
