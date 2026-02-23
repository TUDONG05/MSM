using Microsoft.AspNetCore.Mvc;
using MobileStoreManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace MobileStoreManagement.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _db;


        public OrderController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("revenue")]
        public async Task<IActionResult> GetTotalRevenre()
        {
            var total = await _db.Orders.Where(o => o.Status == "Completed")
                .SumAsync(o => (int)o.TotalAmount) ;
            return Ok(total);
        }

    }
}
