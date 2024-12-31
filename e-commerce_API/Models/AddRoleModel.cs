using System.ComponentModel.DataAnnotations;

namespace e_commerce_API.Models
{
    public class AddRoleModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
