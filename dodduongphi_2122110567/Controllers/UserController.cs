using dodduongphi_2122110567.Model;
using Microsoft.AspNetCore.Mvc;

namespace dodduongphi_2122110567.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // Danh sách user tạm thời
        private static List<User> _users = new List<User>
        {
            new User
            {
                Id = 1,
                Username = "phi123",
                Password = "123456",
                Email = "phi@example.com",
                FullName = "Đỗ Dương Phi",
                Address = "Sài Gòn",
                Phone = "0900000000",
                Role = "Customer"
            }
        };

        // GET: api/User
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            return Ok(_users);
        }

        // GET api/User/5
        [HttpGet("{id}")]
        public ActionResult<User> GetById(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound("Không tìm thấy người dùng với ID này");
            }
            return Ok(user);
        }

        // POST api/User
        [HttpPost]
        public ActionResult<User> Create([FromBody] User newUser)
        {
            newUser.Id = _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
            _users.Add(newUser);
            return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, newUser);
        }

        // PUT api/User/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] User updatedUser)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound("Không tìm thấy người dùng để cập nhật");
            }

            user.Username = updatedUser.Username;
            user.Password = updatedUser.Password;
            user.Email = updatedUser.Email;
            user.FullName = updatedUser.FullName;
            user.Address = updatedUser.Address;
            user.Phone = updatedUser.Phone;
            user.Role = updatedUser.Role;

            return NoContent();
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound("Không tìm thấy người dùng để xóa");
            }

            _users.Remove(user);
            return NoContent();
        }
    }
}
