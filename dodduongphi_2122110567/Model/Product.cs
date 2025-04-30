using dodduongphi_2122110567.Model;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public double Price { get; set; }
    public int Qty { get; set; } // Thêm dòng này để fix lỗi

    public int CategoryId { get; set; }
    public Category? Category { get; set; } // Cho phép null

    public List<OrderDetail> OrderDetails { get; set; } = new(); // Khởi tạo danh sách
}

