using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public Category Category { get; set; }
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public Brand Brand { get; set; }
        [ForeignKey("Brand")]
        public Guid BrandId { get; set; }
        [Range(0, 100)]
        public int Count { get; set; } = 10;
    }
}
