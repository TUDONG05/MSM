using System.ComponentModel.DataAnnotations;

namespace MobileStoreManagement.DTOs
{
    public class ProductUpdateDto
    {
        [Required]
        public string ProductName { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        public int BrandId { get; set; }
    }
}