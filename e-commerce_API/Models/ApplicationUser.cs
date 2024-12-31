using Microsoft.AspNetCore.Identity;

namespace e_commerce_API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Order> orders { get; set; }
    }
}
