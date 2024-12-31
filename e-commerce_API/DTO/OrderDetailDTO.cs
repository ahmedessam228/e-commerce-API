namespace e_commerce_API.DTO
{
    public class OrderDetailDTO
    {
        public int OrderDetailID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice; 
    }

}
