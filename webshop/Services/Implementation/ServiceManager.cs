using AutoMapper;
using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    internal sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _lazyProductService;
        private readonly Lazy<IBrandService> _lazyBrandService;
        private readonly Lazy<ICategoryService> _lazyCategoriesService;
        private readonly Lazy<IRedisService> _lazyRedisService;
        private readonly Lazy<IUserService> _lazyUserService;
        private readonly Lazy<ITokenService> _lazyTokenService;

        public ServiceManager(IGenericRepository<Product> repositoryProducts,
            IGenericRepository<Brand> repositoryBrands,
            IGenericRepository<Category> repositoryCategories,
            IBasketRepository repositoryBasket,
            IMapper mapper,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _lazyProductService = new Lazy<IProductService>(() => new ProductService(repositoryProducts, mapper));
            _lazyBrandService = new Lazy<IBrandService>(() => new BrandService(repositoryBrands, mapper));
            _lazyCategoriesService = new Lazy<ICategoryService>(() => new CategoryService(repositoryCategories, mapper));
            _lazyRedisService = new Lazy<IRedisService>(() => new RedisService(repositoryBasket, mapper));
            _lazyUserService = new Lazy<IUserService>(() => new UserService(userManager, signInManager, mapper));
            _lazyTokenService = new Lazy<ITokenService>(() => new TokenService(userManager));
        }

        public IProductService ProductService => _lazyProductService.Value;

        public IBrandService BrandService => _lazyBrandService.Value;

        public ICategoryService CategoryService => _lazyCategoriesService.Value;

        public IRedisService RedisService => _lazyRedisService.Value;

        public IUserService UserService => _lazyUserService.Value;

        public ITokenService TokenService => _lazyTokenService.Value;
    }
}
