namespace dodduongphi_2122110567.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Role { get; set; } = "Customer"; // Giá trị mặc định
        public string Avatar { get; set; } = string.Empty; // Thêm trường Avatar

        public List<Order> Orders { get; set; } = new();
    }
}
