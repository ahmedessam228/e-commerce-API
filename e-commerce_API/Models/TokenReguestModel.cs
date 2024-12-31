using System.ComponentModel.DataAnnotations;

namespace e_commerce_API.Models
{
    public class TokenReguestModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
