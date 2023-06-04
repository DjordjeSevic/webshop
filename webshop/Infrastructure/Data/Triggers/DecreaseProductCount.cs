using Domain.Entities.OrderAggregate;
using EntityFrameworkCore.Triggered;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Triggers
{
    public class DecreaseProductCount : IAfterSaveTrigger<OrderItem>
    {
        private readonly StoreContext _storeContext;

        public DecreaseProductCount(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public Task AfterSave(ITriggerContext<OrderItem> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Added)
            {
                var productId = context.Entity.ItemOrdered.ProductItemId;
                var count = context.Entity.Quantity;

                var product = _storeContext.Products.Where(p => p.Id == productId).FirstOrDefault();
                product.Count -= count;
                _storeContext.Update(product);
                _storeContext.SaveChanges();
            };

            return Task.CompletedTask;
        }
    }
}
