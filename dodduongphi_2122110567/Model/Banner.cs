namespace dodduongphi_2122110567.Model
{
    public class Banner
    {
        public int Id { get; set; }                        // ID chính
        public string Name { get; set; }                   // Tên banner
        public string Slug { get; set; }                   // Tên không dấu (để dùng làm URL nếu cần)
        public string Image { get; set; }                  // Đường dẫn ảnh
        public string Link { get; set; }                   // Link khi click vào banner
        public string Position { get; set; }               // Vị trí (ví dụ: homepage, sidebar, etc)
        public string Description { get; set; }            // Mô tả ngắn

        public bool Status { get; set; } = true;           // Trạng thái hiển thị
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }           // Xóa mềm
    }
}
