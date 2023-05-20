using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Identity
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        [Required]
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}