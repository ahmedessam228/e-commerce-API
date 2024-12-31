namespace e_commerce_API.DTO
{
    public class OrderDTO
    {
        public int OrderID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; } = new List<OrderDetailDTO>();
    }
}
