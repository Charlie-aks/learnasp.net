using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dodduongphi_2122110567.Model;
using dodduongphi_2122110567.Data;

namespace dodduongphi_2122110567.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BannerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Banner
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Banner>>> GetBanners()
        {
            return await _context.Banners
                .Where(b => b.DeletedAt == null)
                .ToListAsync();
        }

        // GET: api/Banner/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Banner>> GetBanner(int id)
        {
            var banner = await _context.Banners.FindAsync(id);

            if (banner == null || banner.DeletedAt != null)
            {
                return NotFound();
            }

            return banner;
        }

        // POST: api/Banner
        [HttpPost]
        public async Task<ActionResult<Banner>> CreateBanner(Banner banner)
        {
            banner.CreatedAt = DateTime.UtcNow;
            _context.Banners.Add(banner);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBanner), new { id = banner.Id }, banner);
        }

        // PUT: api/Banner/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBanner(int id, Banner banner)
        {
            if (id != banner.Id)
            {
                return BadRequest();
            }

            var existing = await _context.Banners.FindAsync(id);
            if (existing == null || existing.DeletedAt != null)
            {
                return NotFound();
            }

            existing.Name = banner.Name;
            existing.Slug = banner.Slug;
            existing.Image = banner.Image;
            existing.Link = banner.Link;
            existing.Position = banner.Position;
            existing.Description = banner.Description;
            existing.Status = banner.Status;
            existing.UpdatedBy = banner.UpdatedBy;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Banner/5 (soft delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBanner(int id)
        {
            var banner = await _context.Banners.FindAsync(id);
            if (banner == null || banner.DeletedAt != null)
            {
                return NotFound();
            }

            banner.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
