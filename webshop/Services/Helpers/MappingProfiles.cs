using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.Dtos.Products;
using Domain.Entities;
using Contracts.Dtos.Brands;
using Contracts.Dtos.Categories;
using Contracts.Dtos.Redis;
using Domain.Entities.Identity;
using Contracts.Dtos.Identity;

namespace Services.Helpers
{
    internal class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(p => p.Brand, o => o.MapFrom(s => s.Brand.Name))
                .ForMember(p => p.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(p => p.ImageUrl, o => o.MapFrom<ProductImageUrlResolver>());
            CreateMap<Brand, BrandDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<AppUser, AppUserDto>().ReverseMap();
            CreateMap<RegisterDto, AppUser>().ReverseMap();
        }
    }
}
