using dodduongphi_2122110567.Model;
using Microsoft.AspNetCore.Mvc;

namespace dodduongphi_2122110567.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // Danh sách tạm sản phẩm
        private static List<Product> _products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Laptop",
                Description = "Laptop hiệu suất cao",
                Image = "laptop.png",
                Price = 19999999,
                Qty = 10,
                CategoryId = 1,
                Category = null,
                OrderDetails = new List<OrderDetail>()
            },
            new Product
            {
                Id = 2,
                Name = "Áo thun",
                Description = "Áo thun cotton co giãn",
                Image = "aothun.png",
                Price = 150000,
                Qty = 50,
                CategoryId = 2,
                Category = null,
                OrderDetails = new List<OrderDetail>()
            }
        };

        // GET: api/Product
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return Ok(_products);
        }

        // GET api/Product/5
        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound("Không tìm thấy sản phẩm với ID này");
            return Ok(product);
        }

        // POST api/Product
        [HttpPost]
        public ActionResult<Product> Create([FromBody] Product newProduct)
        {
            newProduct.Id = _products.Max(p => p.Id) + 1;
            newProduct.OrderDetails = new List<OrderDetail>();
            newProduct.Category = null;

            _products.Add(newProduct);
            return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
        }

        // PUT api/Product/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product updatedProduct)
        {
            var existing = _products.FirstOrDefault(p => p.Id == id);
            if (existing == null)
                return NotFound("Không tìm thấy sản phẩm để cập nhật");

            existing.Name = updatedProduct.Name;
            existing.Description = updatedProduct.Description;
            existing.Image = updatedProduct.Image;
            existing.Price = updatedProduct.Price;
            existing.Qty = updatedProduct.Qty;
            existing.CategoryId = updatedProduct.CategoryId;

            return NoContent();
        }

        // DELETE api/Product/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound("Không tìm thấy sản phẩm để xóa");

            _products.Remove(product);
            return NoContent();
        }
    }
}
