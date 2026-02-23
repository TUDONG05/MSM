namespace MobileStoreManagement.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } // Pending, Completed, Cancelled

        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}