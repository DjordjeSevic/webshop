using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IServiceManager
    {
        IProductService ProductService { get; }
        IBrandService BrandService { get; }
        ICategoryService CategoryService { get; }
        IRedisService RedisService { get; }
        IUserService UserService { get; }
        ITokenService TokenService { get; }
    }
}
