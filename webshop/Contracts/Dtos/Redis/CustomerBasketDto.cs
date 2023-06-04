using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dtos.Redis
{
    public class CustomerBasketDto
    {
        public CustomerBasketDto(string id)
        {
            Id = id;
        }

        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();

        public Guid? DeliveryMethodId { get; set; }

        public string ClientSecret { get; set; }

        public string PaymentIntentId { get; set; }

        public decimal ShippingPrice { get; set; }
    }
}
