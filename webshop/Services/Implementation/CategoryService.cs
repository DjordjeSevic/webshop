using AutoMapper;
using Contracts.Dtos.Brands;
using Contracts.Dtos.Categories;
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
    internal class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _repositoryCategories;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> repositoryCategories, IMapper mapper)
        {
            _repositoryCategories = repositoryCategories;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _repositoryCategories.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetGateogryAsync(Guid id)
        {
            var category = await _repositoryCategories.GetByIdAsync(id);
            return _mapper.Map<CategoryDto>(category);
        }
    }
}
