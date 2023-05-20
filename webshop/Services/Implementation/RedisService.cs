using AutoMapper;
using Contracts.Dtos.Redis;
using Domain.Entities;
using Domain.Repositories;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    internal class RedisService : IRedisService
    {
        private readonly IMapper _mapper;
        private readonly IBasketRepository _redis;

        public RedisService(IBasketRepository redis, IMapper mapper)
        {
            _mapper = mapper;
            _redis = redis;
        }

        public async Task<CustomerBasketDto> GetBasketAsync(string basketId)
        {
            return _mapper.Map<CustomerBasketDto>(await _redis.GetBasketAsync(basketId));
        }

        public async Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto basket)
        {
            return _mapper.Map<CustomerBasketDto>(await _redis.UpdateBasketAsync(_mapper.Map<CustomerBasket>(basket)));
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _redis.DeleteBasketAsync(basketId);
        }
    }
}
