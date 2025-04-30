using dodduongphi_2122110567.Model;
using Microsoft.AspNetCore.Mvc;

namespace dodduongphi_2122110567.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private static List<Category> _categories = new List<Category>
        {
            new Category
            {
                Id = 1,
                Name = "Điện tử",
                Slug = "dien-tu",
                Image = "dientu.png",
                Description = "Các sản phẩm điện tử",
                ParentId = 0,
                Status = true,
                CreatedBy = "admin",
                UpdatedBy = "admin",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DeletedAt = default,
                Products = new List<Product>()
            }
        };

        // GET: api/Category
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAll()
        {
            return Ok(_categories.Where(c => c.DeletedAt == default));
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public ActionResult<Category> GetById(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id && c.DeletedAt == default);
            if (category == null)
                return NotFound("Không tìm thấy category với ID này");

            return Ok(category);
        }

        // POST: api/Category
        [HttpPost]
        public ActionResult<Category> Create([FromBody] Category newCategory)
        {
            newCategory.Id = _categories.Any() ? _categories.Max(c => c.Id) + 1 : 1;
            newCategory.CreatedAt = DateTime.UtcNow;
            newCategory.UpdatedAt = DateTime.UtcNow;
            newCategory.DeletedAt = default;
            newCategory.Products = new List<Product>();

            _categories.Add(newCategory);
            return CreatedAtAction(nameof(GetById), new { id = newCategory.Id }, newCategory);
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Category updatedCategory)
        {
            var existing = _categories.FirstOrDefault(c => c.Id == id && c.DeletedAt == default);
            if (existing == null)
                return NotFound("Không tìm thấy category để cập nhật");

            existing.Name = updatedCategory.Name;
            existing.Slug = updatedCategory.Slug;
            existing.Image = updatedCategory.Image;
            existing.Description = updatedCategory.Description;
            existing.ParentId = updatedCategory.ParentId;
            existing.Status = updatedCategory.Status;
            existing.UpdatedBy = updatedCategory.UpdatedBy;
            existing.UpdatedAt = DateTime.UtcNow;

            return NoContent();
        }

        // DELETE: api/Category/5 (Xóa mềm)
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id && c.DeletedAt == default);
            if (category == null)
                return NotFound("Không tìm thấy category để xóa");

            category.DeletedAt = DateTime.UtcNow;
            return NoContent();
        }
    }
}
