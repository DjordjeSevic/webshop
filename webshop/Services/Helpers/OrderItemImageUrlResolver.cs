using AutoMapper;
using AutoMapper.Execution;
using Contracts.Dtos.Orders;
using Domain.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public class OrderItemImageUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public OrderItemImageUrlResolver()
        {

        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ItemOrdered.ImageUrl))
            {
                return "https://localhost:7255/" + source.ItemOrdered.ImageUrl;
            }

            return null;
        }
    }
}
