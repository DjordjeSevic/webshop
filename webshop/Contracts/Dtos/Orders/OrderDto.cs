using Contracts.Dtos.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dtos.Orders
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public Guid deliveryMethodId { get; set; }
        public AddressDto ShipToAddress { get; set; }
    }
}
