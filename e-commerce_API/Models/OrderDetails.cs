using System.Text.Json.Serialization;

namespace e_commerce_API.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Order? Order { get; set; }
        public int? O_id { get; set; }

        
        public Product? Product { get; set; }
        public int ProductId { get; set; }

    }

}
