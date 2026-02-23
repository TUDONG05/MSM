using MobileStoreManagement.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileStoreManagement.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        public bool IsAvailable { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}