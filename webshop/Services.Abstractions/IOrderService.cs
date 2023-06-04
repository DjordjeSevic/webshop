using Domain.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Dtos.Orders;
using Contracts.Dtos.Identity;

namespace Services.Abstractions
{
    public interface IOrderService
    {
        Task<OrderReturnDto> CreateOrderAsync(string buyerEmail, Guid deliveryMethodId, string basketId, AddressDto shippingAddres);
        Task<IEnumerable<OrderReturnDto>> GetOrdersForUserAsync(string buyerEmail);
        Task<OrderReturnDto> GetOrderByIdAsync(Guid orderId);
        Task<OrderReturnDto> GetOrderByIdAsync(Guid orderId, string buyerEmail);
        Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodsAsync();
        Task<IEnumerable<OrderReturnDto>> GetAllOrdersAsync();
    }
}
