using dodduongphi_2122110567.Model;
using Microsoft.AspNetCore.Mvc;

namespace dodduongphi_2122110567.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        // Danh sách giả lập các OrderDetail
        private static List<OrderDetail> _orderDetails = new List<OrderDetail>
        {
            new OrderDetail
            {
                Id = 1,
                OrderId = 1,
                Order = new Order { Id = 1 },
                ProductId = 2,
                Product = new Product { Id = 2, Name = "Áo thun", Price = 150000 },
                Quantity = 3,
                UnitPrice = 150000
            }
        };

        // GET: api/OrderDetail - Lấy tất cả chi tiết đơn hàng
        [HttpGet]
        public ActionResult<IEnumerable<OrderDetail>> GetAll()
        {
            return Ok(_orderDetails);
        }

        // GET: api/OrderDetail/5 - Lấy chi tiết đơn hàng theo ID
        [HttpGet("{id}")]
        public ActionResult<OrderDetail> GetById(int id)
        {
            var detail = _orderDetails.FirstOrDefault(d => d.Id == id);
            if (detail == null)
                return NotFound("Không tìm thấy chi tiết đơn hàng");

            return Ok(detail);
        }

        // POST: api/OrderDetail - Thêm chi tiết đơn hàng
        [HttpPost]
        public ActionResult<OrderDetail> Create([FromBody] OrderDetail newDetail)
        {
            newDetail.Id = _orderDetails.Any() ? _orderDetails.Max(d => d.Id) + 1 : 1;
            _orderDetails.Add(newDetail);

            return CreatedAtAction(nameof(GetById), new { id = newDetail.Id }, newDetail);
        }

        // PUT: api/OrderDetail/5 - Cập nhật chi tiết đơn hàng
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] OrderDetail updatedDetail)
        {
            var existing = _orderDetails.FirstOrDefault(d => d.Id == id);
            if (existing == null)
                return NotFound("Không tìm thấy chi tiết đơn hàng để cập nhật");

            existing.OrderId = updatedDetail.OrderId;
            existing.ProductId = updatedDetail.ProductId;
            existing.Quantity = updatedDetail.Quantity;
            existing.UnitPrice = updatedDetail.UnitPrice;

            return NoContent();
        }

        // DELETE: api/OrderDetail/5 - Xóa chi tiết đơn hàng
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var detail = _orderDetails.FirstOrDefault(d => d.Id == id);
            if (detail == null)
                return NotFound("Không tìm thấy chi tiết đơn hàng để xóa");

            _orderDetails.Remove(detail);
            return NoContent();
        }
    }
}
