using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderAggregate
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {

        }

        public ProductItemOrdered(Guid productItemId, string productName, string imageUrl)
        {
            ProductItemId = productItemId;
            ProductName = productName;
            ImageUrl = imageUrl;
        }

        public Guid ProductItemId { get; set; }

        public string ProductName { get; set; }

        public string ImageUrl { get; set; }
    }
}
