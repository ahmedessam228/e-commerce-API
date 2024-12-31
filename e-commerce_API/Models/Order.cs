using Microsoft.Identity.Client;
using System.Text.Json.Serialization;

namespace e_commerce_API.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public double TotalAmount { get; set; }

        public string? UserId { get; set; }
        [JsonIgnore]
        public ApplicationUser? ApplicationUser { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}