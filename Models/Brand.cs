using System.ComponentModel.DataAnnotations;

namespace MobileStoreManagement.Models
{
    public class Brand
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string BrandName { get; set; }

        [MaxLength(100)]
        public string? Country { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Product>? Products { get; set; }
    }
}