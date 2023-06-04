using AutoMapper;
using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Entities.OrderAggregate;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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
        private readonly Lazy<IOrderService> _lazyOrderService;
        private readonly Lazy<IPaymentService> _lazyPaymentService;

        public ServiceManager(IGenericRepository<Product> repositoryProducts,
            IGenericRepository<Brand> repositoryBrands,
            IGenericRepository<Category> repositoryCategories,
            IGenericRepository<Order> repositoryOrders,
            IGenericRepository<DeliveryMethod> repositoryDeliveryMethods,
            IBasketRepository repositoryBasket,
            IMapper mapper,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IConfiguration config)
        {
            _lazyProductService = new Lazy<IProductService>(() => new ProductService(repositoryProducts, mapper));
            _lazyBrandService = new Lazy<IBrandService>(() => new BrandService(repositoryBrands, mapper));
            _lazyCategoriesService = new Lazy<ICategoryService>(() => new CategoryService(repositoryCategories, mapper));
            _lazyRedisService = new Lazy<IRedisService>(() => new RedisService(repositoryBasket, mapper));
            _lazyUserService = new Lazy<IUserService>(() => new UserService(userManager, signInManager, mapper));
            _lazyTokenService = new Lazy<ITokenService>(() => new TokenService(userManager));
            _lazyOrderService = new Lazy<IOrderService>(() => new OrderService(repositoryOrders, repositoryDeliveryMethods, repositoryProducts, repositoryBasket, mapper));
            _lazyPaymentService = new Lazy<IPaymentService>(() => new PaymentService(repositoryBasket, config, repositoryDeliveryMethods, repositoryOrders, mapper));
        }

        public IProductService ProductService => _lazyProductService.Value;

        public IBrandService BrandService => _lazyBrandService.Value;

        public ICategoryService CategoryService => _lazyCategoriesService.Value;

        public IRedisService RedisService => _lazyRedisService.Value;

        public IUserService UserService => _lazyUserService.Value;

        public ITokenService TokenService => _lazyTokenService.Value;

        public IOrderService OrderService => _lazyOrderService.Value;

        public IPaymentService PaymentService => _lazyPaymentService.Value;
    }
}
