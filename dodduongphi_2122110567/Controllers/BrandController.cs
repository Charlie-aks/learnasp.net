using Microsoft.AspNetCore.Mvc;
using dodduongphi_2122110567.Model;
using Microsoft.EntityFrameworkCore;
using dodduongphi_2122110567.Data;

namespace dodduongphi_2122110567.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BrandController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Brand
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            return await _context.Brands.Where(b => b.DeletedAt == default).ToListAsync();
        }

        // GET: api/Brand/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);

            if (brand == null || brand.DeletedAt != default)
            {
                return NotFound();
            }

            return brand;
        }

        // POST: api/Brand
        [HttpPost]
        public async Task<ActionResult<Brand>> CreateBrand(Brand brand)
        {
            brand.CreatedAt = DateTime.UtcNow;
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBrand), new { id = brand.Id }, brand);
        }

        // PUT: api/Brand/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(int id, Brand brand)
        {
            if (id != brand.Id)
            {
                return BadRequest();
            }

            var existing = await _context.Brands.FindAsync(id);
            if (existing == null || existing.DeletedAt != default)
            {
                return NotFound();
            }

            existing.Name = brand.Name;
            existing.Slug = brand.Slug;
            existing.Image = brand.Image;
            existing.Description = brand.Description;
            existing.Status = brand.Status;
            existing.UpdatedBy = brand.UpdatedBy;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Brand/5 (soft delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null || brand.DeletedAt != default)
            {
                return NotFound();
            }

            brand.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
