using AutoMapper;
using Contracts.Dtos.Errors;
using Contracts.Dtos.Identity;
using Contracts.Dtos.Orders;
using Domain.Entities.OrderAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IServiceManager _serviceManager;

        public OrdersController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        public async Task<ActionResult<OrderReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);

            var order = await _serviceManager.OrderService.CreateOrderAsync(email, orderDto.deliveryMethodId, orderDto.BasketId, orderDto.ShipToAddress);

            if (order == null) return BadRequest(new ApiResponse(400, "Problem creating order"));

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderReturnDto>>> GetOrdersForUser()
        {
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);

            var orders = await _serviceManager.OrderService.GetOrdersForUserAsync(email);

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<OrderReturnDto>>> GetOrderByIdForUser(Guid id)
        {
            var role = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);

            var order = role == "Admin" 
                ? await _serviceManager.OrderService.GetOrderByIdAsync(id)
                : await _serviceManager.OrderService.GetOrderByIdAsync(id, email);

            if (order == null) return NotFound(new ApiResponse(404));

            return Ok(order);
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethod>>> GetDeliveryMethods()
        {
            var deliveryMethods = await _serviceManager.OrderService.GetDeliveryMethodsAsync();

            return Ok(deliveryMethods);
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("getAllOrders")]
        public async Task<ActionResult<IEnumerable<OrderReturnDto>>> GetAllOrders()
        {
            var orders = await _serviceManager.OrderService.GetAllOrdersAsync();

            return Ok(orders);
        }
    }
}
