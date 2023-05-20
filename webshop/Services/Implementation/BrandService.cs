using AutoMapper;
using Contracts.Dtos.Brands;
using Contracts.Dtos.Products;
using Domain.Entities;
using Domain.Repositories;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    internal class BrandService : IBrandService
    {
        private readonly IGenericRepository<Brand> _repositoryBrands;
        private readonly IMapper _mapper;

        public BrandService(IGenericRepository<Brand> repositoryBrands, IMapper mapper)
        {
            _repositoryBrands = repositoryBrands;
            _mapper = mapper;
        }

        public async Task<BrandDto> GetBrandAsync(Guid id)
        {
            var brand = await _repositoryBrands.GetByIdAsync(id);
            return _mapper.Map<BrandDto>(brand);
        }

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            var brands = await _repositoryBrands.GetAllAsync();
            return _mapper.Map<IEnumerable<BrandDto>>(brands);
        }
    }
}
