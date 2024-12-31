using System.Text.Json.Serialization;

namespace e_commerce_API.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        [JsonIgnore]
        public Category? category { get; set; }
        public int? C_id { get; set; }
    }
}
