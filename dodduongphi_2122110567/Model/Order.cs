namespace dodduongphi_2122110567.Model
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } // "Pending", "Completed", "Cancelled", etc.
        public string ShippingAddress { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
