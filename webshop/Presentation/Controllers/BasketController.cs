using Contracts.Dtos.Redis;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IServiceManager _serviceManager;

        public BasketController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasketDto>> GetBasketByIdAsync(string id)
        {
            var basket = await _serviceManager.RedisService.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasketDto(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasketAsync(CustomerBasketDto basket)
        {
            var updatedBasket = await _serviceManager.RedisService.UpdateBasketAsync(basket);
            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasketAsync(string id)
        {
            await _serviceManager.RedisService.DeleteBasketAsync(id);
        }
    }
}
