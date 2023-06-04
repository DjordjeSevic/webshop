using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.OrderAggregate;

namespace Persistence.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if (!context.Brands.Any())
            {
                var brandsRaw = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<Brand>>(brandsRaw);
                context.Brands.AddRange(brands);
            }

            if (!context.Categories.Any())
            {
                var categoriesRaw = File.ReadAllText("../Infrastructure/Data/SeedData/categories.json");
                var categories = JsonSerializer.Deserialize<List<Category>>(categoriesRaw);
                context.Categories.AddRange(categories);
            }

            if (!context.Products.Any())
            {
                var productsRaw = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsRaw);
                context.Products.AddRange(products);
            }

            if (!context.DeliveryMethods.Any())
            {
                var deliveryRaw = File.ReadAllText("../Infrastructure/Data/SeedData/delivery.json");
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryRaw);
                context.DeliveryMethods.AddRange(deliveryMethods);
            }

            if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }
    }
}
