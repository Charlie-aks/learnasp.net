namespace dodduongphi_2122110567.Model
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }                 // Tên danh mục
        public string Slug { get; set; }                 // Tên không dấu dùng làm URL
        public string Image { get; set; }                // Ảnh đại diện
        public string Description { get; set; }          // Mô tả

        public int ParentId { get; set; }               // Cho danh mục cha (null nếu là cha)
        public bool Status { get; set; } = true;         // Kích hoạt / ẩn danh mục

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }         // null nếu chưa cập nhật
        public DateTime DeletedAt { get; set; }         // null nếu chưa bị xóa mềm

        // Navigation Property
        public ICollection<Product> Products { get; set; }
    }
}
