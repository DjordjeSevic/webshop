using Contracts.Dtos.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dtos.Orders
{
    public class OrderReturnDto
    {
        public Guid Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public AddressDto ShipToAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
    }
}
