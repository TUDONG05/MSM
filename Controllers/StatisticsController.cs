using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileStoreManagement.Data;

namespace MobileStoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public StatisticsController(AppDbContext db)
        {
            _db = db;
        }

      

        [HttpGet("product-by-brand")]
        public async Task<IActionResult> ProductByBrand()
        {
            var kq = await _db.Products
                .Include(p => p.Brand)
                .GroupBy(p => p.Brand.BrandName)
                .Select(g => new
                {
                    BrandName = g.Key,
                    Total = g.Count()
                }
                ).ToListAsync();
            return Ok(kq);
        }

        [HttpGet("total-stock")]
        public async Task <IActionResult> TotalStock()
        {
            var total = await _db.Products.SumAsync(p => p.Stock);
            return Ok(new { TotalStock = total });
        }
    }
}