using AutoMapper;
using Contracts.Dtos.Redis;
using Domain.Entities.OrderAggregate;
using Domain.Repositories;
using Domain.Specifications;
using Microsoft.Extensions.Configuration;
using Services.Abstractions;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    internal class PaymentService : IPaymentService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IConfiguration _config;
        private readonly IGenericRepository<DeliveryMethod> _dmRepository;
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IMapper _mapper;

        public PaymentService(IBasketRepository basketRepository, IConfiguration config, IGenericRepository<DeliveryMethod> dmRepository, IGenericRepository<Order> orderRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _config = config;
            _dmRepository = dmRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];

            var basket = await _basketRepository.GetBasketAsync(basketId);
            var shippingPrice = 0m;

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _dmRepository.GetByIdAsync((Guid)basket.DeliveryMethodId);
                shippingPrice = deliveryMethod.Price;
            }

            var service = new PaymentIntentService();

            PaymentIntent intent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                intent = await service.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100
                };
                await service.UpdateAsync(basket.PaymentIntentId, options);
            }

            await _basketRepository.UpdateBasketAsync(basket);

            return _mapper.Map<CustomerBasketDto>(basket);
        }

        public async Task<Order> UpdateOrderPaymentFailed(string paymentIntentId)
        {
            var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);
            var order = await _orderRepository.GetEntityWithSpec(spec);

            if (order == null) return null;

            order.Status = OrderStatus.PaymentFailed;
            var res = await _orderRepository.Update(order);
            if (res <= 0) return null;

            return order;
        }

        public async Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId)
        {
            var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);
            var order = await _orderRepository.GetEntityWithSpec(spec);

            if (order == null) return null;

            order.Status = OrderStatus.PaymentReceived;
            var res = await _orderRepository.Update(order);
            if (res <= 0) return null;

            return order;
        }
    }
}
