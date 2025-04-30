using dodduongphi_2122110567.Model;
using Microsoft.AspNetCore.Mvc;

namespace dodduongphi_2122110567.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private static List<Order> _orders = new List<Order>
        {
            new Order
            {
                Id = 1,
                UserId = 1,
                User = new User { Id = 1, FullName = "Nguyễn Văn A", Email = "a@example.com" },
                OrderDate = DateTime.UtcNow,
                TotalAmount = 1000000,
                Status = "Pending",
                ShippingAddress = "123 Đường ABC, Quận 1, TP.HCM",
                OrderDetails = new List<OrderDetail>()
            }
        };

        // GET: api/Order
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAll()
        {
            return Ok(_orders);
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public ActionResult<Order> GetById(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return NotFound("Không tìm thấy đơn hàng");

            return Ok(order);
        }

        // POST: api/Order
        [HttpPost]
        public ActionResult<Order> Create([FromBody] Order newOrder)
        {
            newOrder.Id = _orders.Any() ? _orders.Max(o => o.Id) + 1 : 1;
            newOrder.OrderDate = DateTime.UtcNow;
            _orders.Add(newOrder);

            return CreatedAtAction(nameof(GetById), new { id = newOrder.Id }, newOrder);
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Order updatedOrder)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.Id == id);
            if (existingOrder == null)
                return NotFound("Không tìm thấy đơn hàng để cập nhật");

            existingOrder.UserId = updatedOrder.UserId;
            existingOrder.User = updatedOrder.User;
            existingOrder.TotalAmount = updatedOrder.TotalAmount;
            existingOrder.Status = updatedOrder.Status;
            existingOrder.ShippingAddress = updatedOrder.ShippingAddress;
            existingOrder.OrderDetails = updatedOrder.OrderDetails;

            return NoContent();
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return NotFound("Không tìm thấy đơn hàng để xóa");

            _orders.Remove(order);
            return NoContent();
        }
    }
}
