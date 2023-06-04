using AutoMapper;
using Contracts.Dtos.Identity;
using Contracts.Dtos.Orders;
using Domain.Entities;
using Domain.Entities.OrderAggregate;
using Domain.Repositories;
using Domain.Specifications;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    internal class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepo;
        private readonly IGenericRepository<DeliveryMethod> _dmRepo;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IBasketRepository _basketRepo;
        private readonly IMapper _mapper;

        public OrderService(IGenericRepository<Order> orderRepo, IGenericRepository<DeliveryMethod> dmRepo, IGenericRepository<Product> productRepo, IBasketRepository basketRepo, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _dmRepo = dmRepo;
            _productRepo = productRepo;
            _basketRepo = basketRepo;
            _mapper = mapper;
        }

        public async Task<OrderReturnDto> CreateOrderAsync(string buyerEmail, Guid deliveryMethodId, string basketId, AddressDto shippingAddres)
        {
            var basket = await _basketRepo.GetBasketAsync(basketId);

            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await _productRepo.GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.ImageUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            var deliveryMethods = await _dmRepo.GetByIdAsync(deliveryMethodId);

            var subtotal = items.Sum(item => item.Price * item.Quantity);

            var address = _mapper.Map<AddressDto, Address>(shippingAddres);

            var spec = new OrderByPaymentIntentIdSpecification(basket.PaymentIntentId);

            var order = await _orderRepo.GetEntityWithSpec(spec);

            if (order != null)
            {
                order.ShipToAddress = _mapper.Map<Address>(shippingAddres);
                order.DeliveryMethod = deliveryMethods;
                order.Subtotal = subtotal;
                var result = await _orderRepo.Update(order);

                if (result <= 0) return null;
            }
            else
            {
                order = new Order(items, buyerEmail, _mapper.Map<AddressDto,Address>(shippingAddres), deliveryMethods,
                    subtotal, basket.PaymentIntentId);
                var result = await _orderRepo.Add(order);

                if (result <= 0) return null;
            }

            return _mapper.Map<OrderReturnDto>(order);
        }

        public async Task<IEnumerable<OrderReturnDto>> GetAllOrdersAsync()
        {
            var spec = new OrdersWithItemsAndOrderingSpecification();

            return _mapper.Map<IEnumerable<OrderReturnDto>>(await _orderRepo.GetAsync(spec));
        }

        public async Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _dmRepo.GetAllAsync();
        }

        public async Task<OrderReturnDto> GetOrderByIdAsync(Guid orderId)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(orderId);

            var res = await _orderRepo.GetEntityWithSpec(spec);

            return _mapper.Map<OrderReturnDto>(res);
        }

        public async Task<OrderReturnDto> GetOrderByIdAsync(Guid orderId, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(orderId, buyerEmail);

            var res = await _orderRepo.GetEntityWithSpec(spec);

            return _mapper.Map<OrderReturnDto>(res);
        }

        public async Task<IEnumerable<OrderReturnDto>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);

            return _mapper.Map<IEnumerable<OrderReturnDto>>(await _orderRepo.GetAsync(spec));
        }
    }
}
