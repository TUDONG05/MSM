using MobileStoreManagement.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileStoreManagement.DTOs
{
    public class GetProductsDTO
    {

        public int Id { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public string BrandName { get; set; }


    }
}
