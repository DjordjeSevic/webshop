using AutoMapper.Execution;
using Contracts.Dtos.Products;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;


namespace Services.Helpers
{
    internal class ProductImageUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        public ProductImageUrlResolver()
        {
        }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImageUrl))
            {
                return "https://localhost:7255/" + source.ImageUrl;
            }

            return null;
        }
    }
}
