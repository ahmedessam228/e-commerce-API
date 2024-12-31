using System.ComponentModel.DataAnnotations;

namespace e_commerce_API.Models
{
    public class RegisterModel
    {
        [Required, StringLength(50)]
        public string Username { get; set; }
        [Required, StringLength(100)]
        public string Email { get; set; }
        [Required, StringLength(150)]
        public string Password { get; set; }
    }
}
