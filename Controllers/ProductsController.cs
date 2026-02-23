using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileStoreManagement.Data;
using MobileStoreManagement.DTOs;
using MobileStoreManagement.Models;

namespace MobileStoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProductsController(AppDbContext db)
        {
            _db = db;
        }


        // Lấy tất cả sản phẩm
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _db.Products
                .Include(p => p.Brand)
                //.Where(p => p.Stock >5)
                .Select(p => new GetProductsDTO
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Stock = p.Stock,
                    BrandName = p.Brand.BrandName
                }).ToListAsync();

            return Ok(products);
        }



        //Lấy sp theo id

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var sp = await _db.Products
                .Include(p => p.Brand)
                .Where(p => p.Id == id)
                .Select(p => new GetProductsDTO
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Stock = p.Stock,
                    BrandName = p.Brand.BrandName
                }).FirstOrDefaultAsync();

            if (sp == null)
                return NotFound();
            return Ok(sp);
        }


        //thêm sản phẩm 

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Kiểm tra đã tồn tại chưa trước khi thêm  

            bool exits = await _db.Products.AnyAsync(p => p.ProductName == dto.ProductName && p.BrandId == dto.BrandId);

            if (exits)
                return Conflict("San pham nay da ton tai");


            var sp = new Product
            {
                ProductName = dto.ProductName,
                Price = dto.Price,
                Stock = dto.Stock,
                BrandId = dto.BrandId,
                IsAvailable = (dto.Stock > 0)
            };

            _db.Products.Add(sp);
            _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProductById), new { id = sp.Id }, sp);
        }






        


        [HttpPut("{id}")]
        public async Task <IActionResult> UpdateProduct(int  id, ProductUpdateDto dto)
        {
            var sp = await _db.Products.FindAsync(id);

            if (sp == null)
                return NotFound();

            if (dto.Price < 0)
                return BadRequest("Khong cho phep Price am");

            sp.ProductName = dto.ProductName;
            sp.Price = dto.Price;
            sp.Stock = dto.Stock;
            sp.BrandId = dto.BrandId;
            sp.IsAvailable = dto.Stock > 0;

            await _db.SaveChangesAsync();
            return Ok(sp);

        }

        //Xoa sp theo id tìm dc
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var sp = await _db.Products.FindAsync(id);

            if (sp == null)
                return NotFound();

            bool ExistsInOrder = await _db.OrderDetails.AnyAsync(od => od.ProductId == id);

            if (ExistsInOrder)
                return Conflict("Khong xoa san pham da co trong OderDetail");

            _db.Products.Remove(sp);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}