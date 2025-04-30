using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dodduongphi_2122110567.Model
{
    public class Brand
    {
        public int Id { get; set; }

        public string Name { get; set; }                 // Tên thương hiệu
        public string Slug { get; set; }                 // Tên không dấu dùng làm URL
        public string Image { get; set; }                // Ảnh logo thương hiệu
        public string Description { get; set; }          // Mô tả

        public bool Status { get; set; } = true;         // Kích hoạt / ẩn thương hiệu

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }          // null nếu chưa cập nhật
        public DateTime DeletedAt { get; set; }          // null nếu chưa bị xóa mềm

    }
}
